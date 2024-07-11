namespace ObjectCreator.Services
{
	public class Manager:IManager
	{
		private readonly ICache _cache;
		private readonly ICalculator _calculator;
		private int _count;

		public Manager(ICache cache, ICalculator calculator)
		{
			_cache = cache;
			_calculator = calculator;
		}

		public async void Manage()
		{
			int result = await _calculator.Calculate(3, 4);
            Console.Out.WriteLine(result);
        }
	}
}
