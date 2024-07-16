using Moq;
using Newtonsoft.Json;
using System.Reflection;

namespace UnitTests
{
	public class Base<T> where T : class
	{
		static Dictionary<Type, FieldInfo> _mockFields;
		static Dictionary<Type, FieldInfo> _classDependencies;
		static Dictionary<Type, (object original, int argPosition)> _originalClassInstances;

		ConstructorInfo _ctor;
		object[] _arguments;

		protected T _system;


		public Base()
		{
			if (_mockFields is not null) return;

			_mockFields = new Dictionary<Type, FieldInfo>();
			_classDependencies = new Dictionary<Type, FieldInfo>();
			_originalClassInstances = new Dictionary<Type, (object original, int argPosition)>();


			var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

			foreach (var field in fields)
			{
				bool isMock = field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(Mock<>);

				if (isMock)
					_mockFields.Add(field.FieldType.GenericTypeArguments[0], field);
				else
					_classDependencies.Add(field.FieldType, field);
			}


			var type = typeof(T);
			_ctor = type.GetConstructors().FirstOrDefault()!;

			if (_ctor is null)
			{
				//public parameterless
				_ctor = type.GetConstructor(Type.EmptyTypes)!;
			}

			var params_ = _ctor.GetParameters();

			var arguments = new List<object>();

			for (int i = 0; i < params_.Length; i++)
			{
				var param = params_[i];

				if (param.ParameterType.IsInterface)
				{
					var mockType = typeof(Mock<>).MakeGenericType(param.ParameterType);
					var mockCtor = mockType.GetConstructor([typeof(MockBehavior)])!;

					var mock = mockCtor.Invoke([MockBehavior.Strict]);

					// both Mock<> and Mock has an Object prop
					var obj = mockType.GetProperties().Single(prop => prop.Name == "Object" && prop.DeclaringType == mockType).GetGetMethod()!.Invoke(mock, null)!;

					arguments.Add(obj);

					_mockFields[param.ParameterType].SetValue(this, mock);
				}
				else if (param.ParameterType.IsClass)
				{
					//TODO: bonyodalmak, ha nem egy sima config osztály hanem ennek is vannak dependái, akkor rekurzív genyó kell
					var ctor = param.ParameterType.GetConstructor(Type.EmptyTypes)!;

					var obj = ctor.Invoke(null);

					arguments.Add(obj);
					
					_classDependencies[param.ParameterType].SetValue(this, obj);

					_originalClassInstances.Add(param.ParameterType, (obj, i));
				}
			}

			_arguments = arguments.ToArray();
		}


		[SetUp]
		public virtual void BaseSetUp()
		{
			foreach (var mock in _mockFields.Values)
			{
				MockExtensions.Reset((mock.GetValue(this) as Mock)!);
			}

			foreach (var kvp in _originalClassInstances)
			{
				//cheap deep copy
				var json =JsonConvert.SerializeObject(kvp.Value.original);
				var clone = JsonConvert.DeserializeObject(json, kvp.Key)!;

				_classDependencies[kvp.Key].SetValue(this, clone);
				_arguments[kvp.Value.argPosition] = clone;
			}

			_system = CreateInstance();
		}


		[TearDown]
		public virtual void BaseTearDown()
		{
			foreach(var field in _mockFields.Values)
			{
				var mock = field.GetValue(this)!;
				mock.GetType().GetMethod(nameof(Mock.VerifyAll))!.Invoke(mock,null);
			}
		}


		protected T CreateInstance()
		{
			var instance = _ctor.Invoke(_arguments);

			return (T)instance;
		}
	}
}
