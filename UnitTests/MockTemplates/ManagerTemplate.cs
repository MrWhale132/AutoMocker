using Moq;
using ObjectCreator.Configs;
using ObjectCreator.Services;

namespace UnitTests.MockTemplates
{
    internal class ManagerTemplate : Base<Manager>
    {
        protected Config _config;
        protected Mock<ICalculator> _calculator;
        protected Mock<ICache> _cache;
        protected Mock<IGeneric<int, ICache>> _generic;
    }
}
