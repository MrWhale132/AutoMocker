using Moq;
using ObjectCreator.Services;
using System.Reflection;

namespace ObjectCreator
{
	internal class Program
	{
		static void Main(string[] args)
		{


			FieldInfo[] fields = typeof(Manager).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

			var test = Create<Manager>();

			//test.Manage();
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


	class Test
	{

	}
}
