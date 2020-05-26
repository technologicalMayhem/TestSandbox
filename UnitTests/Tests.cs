using NUnit.Framework;
using TestProject;

namespace UnitTests
{
    public class Tests
    {
        private TestClass _testClass;
        
        [SetUp]
        public void Setup()
        {
            _testClass = new TestClass();
        }

        [Test]
        public void NumberTest()
        {
            for (var i = 0; i < 10; i++)
            {
                Assert.AreEqual(_testClass.ReturnNumber(i), i);
            }
        }
    }
}