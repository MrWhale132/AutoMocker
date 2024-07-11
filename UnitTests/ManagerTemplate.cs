using Moq;
using ObjectCreator.Services;

namespace UnitTests
{
	internal class ManagerTemplate : Base<Manager>
	{
		protected Mock<ICalculator> _calculator;
		protected Mock<ICache> _cache;
	}
}
