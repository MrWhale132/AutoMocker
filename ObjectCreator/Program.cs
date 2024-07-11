using Moq;
using ObjectCreator.Services;
using System.Reflection;

namespace ObjectCreator
{
	internal class Program
	{
		static void Main(string[] args)
		{

			MyGenericClass<string> instance = new MyGenericClass<string>();

			// Set the value of 'myField' to 42 using the reflection method
			instance.SetFieldUsingReflection(42);

			// Print the value of 'myField' to verify it has been set correctly
			Console.WriteLine(instance);  // Output: myField: 42




			FieldInfo[] fields = typeof(Manager).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

			var test = Create<Manager>();

			test.Manage();
		}

		static T Create<T>() where T : class
		{
			var type = typeof(T);

			ConstructorInfo ctor = type.GetConstructors().First();


			var test = typeof(Mock<>);

			test.GetConstructor(Type.EmptyTypes);

			Type generic = test.MakeGenericType(typeof(ICache));

			var genctor=generic.GetConstructor([typeof(MockBehavior)]);

			object mock = genctor.Invoke([MockBehavior.Strict]);

			Mock<ICache> cache = (Mock<ICache>)mock;



            Console.WriteLine(test.FullName);


            object instance = ctor.Invoke([null,null]);

			return (T) instance;
		}
	}

	public class MyGenericClass<T>
	{
		public int myField;

		public void SetFieldUsingReflection(int value)
		{
			// Get the FieldInfo for the field 'myField'
			FieldInfo fieldInfo = typeof(MyGenericClass<T>).GetField("myField", BindingFlags.Public | BindingFlags.Instance);

			// Set the value of 'myField' using reflection
			fieldInfo.SetValue(this, value);
		}

		public override string ToString()
		{
			return $"myField: {myField}";
		}
	}

	class Test
	{

	}
}
