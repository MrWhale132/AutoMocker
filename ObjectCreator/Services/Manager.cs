using ObjectCreator.Configs;

namespace ObjectCreator.Services
{
	public class Manager:IManager
	{
		private readonly ICache _cache;
		private readonly ICalculator _calculator;
		private readonly Config _config;
		private readonly IGeneric<int, ICache> _generic;


		public Manager(ICache cache, ICalculator calculator, Config config, IGeneric<int, ICache> generic)
		{
			_cache = cache;
			_calculator = calculator;
			_config = config;
			_generic = generic;
		}

		public async Task Manage()
		{
			int result = await _calculator.Calculate(3, 4);

			await _generic.GenTest();

			await _generic.GenMethodTest<ICache>();
        }
	}
}
