using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Updog.Domain.Tests {
    /// <summary>
    /// Unit tests for the user entity.
    /// </summary>
    [TestClass]
    public class UserTests {
        #region Publics
        [TestMethod]
        public void SetPasswordRejectsBadCurrentPassword() {
            var user = new User(new MockPasswordHasher(), "Mock", "password");

            Assert.ThrowsException<UnauthorizedAccessException>(() => {
                user.SetPassword("notpassword", "testpassword");
            });
        }

        [TestMethod]
        public void SetPasswordAcceptsCurrentPassword() {
            var user = new User(new MockPasswordHasher(), "Mock", "password");

            user.SetPassword("password", "password2");
            Assert.AreEqual(user.PasswordHash, "password2");
        }

        [TestMethod]
        public void ResetPasswordChangesPassword() {
            var user = new User(new MockPasswordHasher(), "Mock", "password");

            user.ResetPassword("password2");
            Assert.AreEqual(user.PasswordHash, "password2");
        }

        [TestMethod]
        public void AuthenticateFailsMismatch() {
            var user = new User(new MockPasswordHasher(), "Mock", "password");
            Assert.IsFalse(user.Authenticate("password2"));
        }

        [TestMethod]
        public void AuthenticateAcceptsMatch() {
            var user = new User(new MockPasswordHasher(), "Mock", "password");
            Assert.IsTrue(user.Authenticate("password"));
        }

        [TestMethod]
        public void UpdateSetsEmail() {
            var user = new User(new MockPasswordHasher(), "Mock", "password");

            user.Update(new UserUpdate("email"));
            Assert.AreEqual(user.Email, "email");
        }
        #endregion        
    }
}
