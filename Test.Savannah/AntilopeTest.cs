using NUnit.Framework;
using Savannah;

namespace Test.Savannah {
    [TestFixture]
    public class AntilopeTest {
        private Antilope antilope;

        [SetUp]
        public void SetUp() {
            antilope = new Antilope();
        }

        [Test]
        public void Antilope_CanCreateNewAntilope_Can() {
            Assert.AreEqual(150, antilope.HitPoints);
        }

        [Test]
        public void Die_SetAntilopeHPTo0_AntilopeDies() {
            antilope.Die();
            Assert.AreEqual(0, antilope.HitPoints);
        }
    }
}