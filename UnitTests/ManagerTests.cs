using Moq;
using ObjectCreator.Services;
using UnitTests.MockTemplates;

namespace UnitTests
{
	internal class ManagerTests : ManagerTemplate
	{
		public override void BaseSetUp()
		{
			base.BaseSetUp();

			_config.Count = 5;
		}

		[Test]
		public async Task Test()
		{
			Console.Out.WriteLine(_calculator.Setups.Count);
		}

		[Test]
		public async Task Test1()
		{
			_calculator.Setup(x => x.Calculate(3, It.IsAny<int>())).ReturnsAsync(100);

			_system.Manage();

			Console.Out.WriteLine(_calculator.Setups.Count);
		}

		[Test]
		public async Task Test2()
		{
			Console.Out.WriteLine(_calculator.Setups.Count);
		}

		[Test]
		public async Task GenTest()
		{
			_calculator.Setup(x => x.Calculate(3, It.IsAny<int>())).ReturnsAsync(100);

			_generic.Setup(x => x.GenTest()).ReturnsAsync(10);

			_generic.Setup(x => x.GenMethodTest<ICache>()).ReturnsAsync(It.IsAny<ICache>());

			await _system.Manage();
		}

	}


	internal class GenericTests : GenericServiceTemplate<int, ICache>
	{
		[Test]
		public async Task Test()
		{
			_generic.Setup(x => x.GenTest()).ReturnsAsync(10);

			await _system.GenTest();
		}
	}


	internal class CacheTests : Base<Cache>
	{
		[Test]
		public async Task Test()
		{
			//paramless ctor test
		}
	}

}
