using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Updog.Domain.Tests {
    /// <summary>
    /// Unit tests for the user entity.
    /// </summary>
    [TestClass]
    public class UserTests {
        [TestMethod]
        public void EqualsTrueIfIdMatch() {
            Assert.IsTrue(new User() { Id = 1 }.Equals(new User() { Id = 1 }));
        }
    }
}
