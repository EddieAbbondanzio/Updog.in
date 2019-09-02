using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Updog.Domain.Tests {
    /// <summary>
    /// Unit tests for the post type flag.
    /// </summary>
    [TestClass]
    public class PostTypeTests {
        /*
        * These two unit tests are designed as an idiot-proof fail
        * safe to ensure they stay in sync with the PostType enum
        * on the front end.
        */

        [TestMethod]
        public void LinkPostIsZero() {
            Assert.AreEqual(0, (int)PostType.Link);
        }

        public void TextPostIsOne() {
            Assert.AreEqual(1, (int)PostType.Text);
        }
    }
}
