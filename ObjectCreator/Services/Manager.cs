using ObjectCreator.Configs;

namespace ObjectCreator.Services
{
	public class Manager:IManager
	{
		private readonly ICache _cache;
		private readonly ICalculator _calculator;
		private readonly Config _config;
		private int _count;

		public Manager(ICache cache, ICalculator calculator, Config config)
		{
			_cache = cache;
			_calculator = calculator;
			_config = config;
		}

		public async void Manage()
		{
			int result = await _calculator.Calculate(3, 4);
            Console.Out.WriteLine(result);
        }
	}
}
