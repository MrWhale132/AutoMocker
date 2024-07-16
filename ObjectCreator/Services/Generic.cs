
namespace ObjectCreator.Services
{
	internal class Generic : IGeneric<int, ICache>
	{
		public Task<V> GenMethodTest<V>()
		{
			throw new NotImplementedException();
		}

		public Task<int> GenTest()
		{
			throw new NotImplementedException();
		}

		public Task Test()
		{
			throw new NotImplementedException();
		}
	}
}
