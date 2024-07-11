using Moq;
using System.Reflection;

namespace UnitTests
{
	internal class Base<T> where T : class
	{
		static Dictionary<Type, FieldInfo> _mocks;

		protected T _system;


		public Base()
		{
			_mocks = new Dictionary<Type, FieldInfo>();

			var mocksFields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
									  .Where(x => x.FieldType.IsGenericType && x.FieldType.GetGenericTypeDefinition() == typeof(Mock<>));

			_mocks = mocksFields.ToDictionary(x => x.FieldType.GenericTypeArguments[0], x => x);
		}



		[SetUp]
		public virtual void BaseSetUp()
		{
            Console.WriteLine("setup");
            _system = Mock();
		}

		[TearDown]
		public virtual void TearDown()
		{
			
		}


		public T Mock()
		{
			var type = typeof(T);
			var ctor = type.GetConstructors().FirstOrDefault();

			if (ctor is null)
			{
				ctor = type.GetConstructor(Type.EmptyTypes);
			}

			var params_ = ctor.GetParameters();

			var mocks = new object[params_.Length];
			var objects= new object[params_.Length];

			for (int i = 0; i < mocks.Length; i++)
			{
				var param = params_[i];

				var mockType = typeof(Mock<>).MakeGenericType(param.ParameterType);
				var mockCtor = mockType.GetConstructor([typeof(MockBehavior)]);

				var mock = mockCtor.Invoke([MockBehavior.Strict]);
				var obj=mockType.GetProperties().Single(prop => prop.Name == "Object" && prop.DeclaringType.Name == mockType.Name).GetGetMethod().Invoke(mock,null);

				mocks[i] = mock;
				objects[i] = obj;

				_mocks[param.ParameterType].SetValue(this, mock);
			}

			var instance = ctor.Invoke(objects);

			return (T)instance;
		}


	}
}
