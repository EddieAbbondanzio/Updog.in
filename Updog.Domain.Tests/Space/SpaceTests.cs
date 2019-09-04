using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Updog.Domain.Tests {
    /// <summary>
    /// Unit tests for the space entity.
    /// </summary>
    [TestClass]
    public class SpaceTests {
        [TestMethod]
        public void EqualsTrueIfIdMatch() {
            Assert.IsTrue(new Space() { Id = 1 }.Equals(new Space() { Id = 1 }));
        }
    }
}
