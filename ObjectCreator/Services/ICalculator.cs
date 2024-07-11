namespace ObjectCreator.Services
{
	public interface ICalculator
	{
		Task<int> Calculate(int x, int y);
	}
}
