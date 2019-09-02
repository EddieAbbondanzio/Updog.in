using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Updog.Domain.Tests {
    /// <summary>
    /// Unit tests for the comment entity.
    /// </summary>
    [TestClass]
    public class CommentTests {
        [TestMethod]
        public void EqualsTrueIfIdMatch() {
            Assert.IsTrue(new Comment() { Id = 1 }.Equals(new Comment() { Id = 1 }));
        }
    }
}
