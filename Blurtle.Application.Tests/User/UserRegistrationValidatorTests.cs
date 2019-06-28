using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Blurtle.Application;
using System.Threading.Tasks;

namespace Blurtle.Application.Tests {
    /// <summary>
    /// Unit tests for the UserRegistrationValidator
    /// </summary>
    [TestClass]
    public class UserRegistrationValidatorTests {
        UserRegistrationValidator validator = new UserRegistrationValidator(new MockUserRepo());

        [TestMethod]
        public async Task FailsUsernameUnder4Characters() {
            UserRegistration reg = new UserRegistration("cat", "password");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsUsernameOver24Characters() {
            UserRegistration reg = new UserRegistration("1234567890123456789012345", "password");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsUsernameIfTaken() {
            UserRegistration reg = new UserRegistration("bert", "password");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        public async Task FailsUsernameIfInvalidCharacters() {
            UserRegistration reg = new UserRegistration("@bert", "password");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsPasswordWhenNull() {
            UserRegistration reg = new UserRegistration("bert", null);
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsPasswordWhenEmpty() {
            UserRegistration reg = new UserRegistration("bert", "         ");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        public async Task FailsPasswordWhenUnder8Characters() {
            UserRegistration reg = new UserRegistration("bert", "cat");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsEmailIfNotNullAndInvalid() {
            UserRegistration reg = new UserRegistration("bert", "password", "fake");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        public async Task FailsEmailIfOver64Characters() {
            UserRegistration reg = new UserRegistration("bert2", "password", "1234567890123456789012345123456789012345678901234512345678901234@fake.com");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task AcceptsValidUser() {
            UserRegistration reg = new UserRegistration("bert2", "password", "fake@mail.com");
            var result = await validator.ValidateAsync(reg);
            Assert.IsTrue(result.IsValid);
        }
    }
}
