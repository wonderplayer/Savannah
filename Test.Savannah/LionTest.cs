using NUnit.Framework;
using Savannah;

namespace Test.Savannah {
    [TestFixture]
    public class LionTest {
        private Lion lion;

        [SetUp]
        public void Setup() {
            lion = new Lion();
        }

        [Test]
        public void Lion_CanCreateLion_Can() {
            Assert.AreEqual(100, lion.HitPoints);
        }

        [Test]
        public void Lion_SetLionHPTo0_LionDies() {
            lion.Die();
            Assert.AreEqual(0, lion.HitPoints);
        }
    }
}