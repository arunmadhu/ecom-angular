using AutoFixture;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace unittests
{
    public class OrderCommandTest
    {
        private IFixture fixture;

        [SetUp]
        public void Setup()
        {
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
