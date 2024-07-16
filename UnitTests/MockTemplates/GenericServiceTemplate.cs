using Moq;
using ObjectCreator.Services;

namespace UnitTests.MockTemplates
{
	public class GenericServiceTemplate<T, U> : Base<GenericService<T, U>>
	{
		protected Mock<IGeneric<T, U>> _generic;
	}
}
