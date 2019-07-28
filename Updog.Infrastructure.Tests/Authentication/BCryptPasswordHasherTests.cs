using Microsoft.VisualStudio.TestTools.UnitTesting;
using Updog.Infrastructure;

namespace Updog.Infrastructure.Tests {
    /// <summary>
    /// Unit tests for the BCryptPasswordHasher.
    /// </summary>
    [TestClass]
    public class BCryptPasswordHasherTests {
        private BCryptPasswordHasher passHasher = new BCryptPasswordHasher();

        /// <summary>
        /// Checks that the hashing function is not storing passwords in plain text.
        /// </summary>
        [TestMethod]
        public void HashIsNotEqualToPassword() {
            Assert.AreNotEqual("hunter2", passHasher.Hash("hunter2"));
        }

        /// <summary>
        /// Check to see if a hash can be correctly verified with it's original password.
        /// </summary>
        [TestMethod]
        public void HashCanBeVerified() {
            string hash = passHasher.Hash("hunter2");
            Assert.IsTrue(passHasher.Verify("hunter2", hash));
        }

        /// <summary>
        /// Check to see that an incorrect password does not match the hash.
        /// </summary>
        [TestMethod]
        public void IncorrectPasswordFailsVerify() {
            string hash = passHasher.Hash("hunter2");
            Assert.IsFalse(passHasher.Verify("hunter3", hash));
        }
    }
}
