
namespace ObjectCreator.Services
{
	public class GenericService<T, U> : IGeneric<T, U>
	{
		private readonly IGeneric<T, U> _generic;

		public GenericService(IGeneric<T, U> generic)
		{
			_generic = generic;
		}


		public Task<V> GenMethodTest<V>()
		{
			throw new NotImplementedException();
		}

		public async Task<T> GenTest()
		{
			return await _generic.GenTest();
		}

		public Task Test()
		{
			throw new NotImplementedException();
		}
	}
}
