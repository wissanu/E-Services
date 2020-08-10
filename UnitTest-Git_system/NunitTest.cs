using NUnit.Framework;

namespace UnitTest_Git_system
{
    [TestFixture]
    internal class NunitTest
    {
        [TestCase(1, 1)]
        public void ex1(int a, int b)
        {
            Assert.AreEqual(a, b, "err");
        }

        [Test]
        public void TestMethod1()
        {
            Assert.AreEqual(true, true, "Data not match.");
        }
    }
}