namespace ObjectCreator.Services
{
	public interface IGeneric<T,U>
	{
		Task Test();

		Task<T> GenTest();

		Task<V> GenMethodTest<V>();
	}
}
