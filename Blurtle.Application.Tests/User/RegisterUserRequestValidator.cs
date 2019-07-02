using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Blurtle.Application;
using System.Threading.Tasks;

namespace Blurtle.Application.Tests {
    /// <summary>
    /// Unit tests for the RegisterUserRequestValidator
    /// </summary>
    [TestClass]
    public class RegisterUserRequestValidator {
        Application.RegisterUserRequestValidator validator = new Application.RegisterUserRequestValidator(new MockUserRepo());

        [TestMethod]
        public async Task FailsUsernameUnder4Characters() {
            RegisterUserRequest reg = new RegisterUserRequest("cat", "password");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsUsernameOver24Characters() {
            RegisterUserRequest reg = new RegisterUserRequest("1234567890123456789012345", "password");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsUsernameIfTaken() {
            RegisterUserRequest reg = new RegisterUserRequest("bert", "password");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsUsernameIfInvalidCharacters() {
            RegisterUserRequest reg = new RegisterUserRequest("@bert", "password");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsPasswordWhenNull() {
            RegisterUserRequest reg = new RegisterUserRequest("bert", null);
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsPasswordWhenEmpty() {
            RegisterUserRequest reg = new RegisterUserRequest("bert", "         ");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsPasswordWhenUnder8Characters() {
            RegisterUserRequest reg = new RegisterUserRequest("bert", "cat");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsEmailIfNotNullAndInvalid() {
            RegisterUserRequest reg = new RegisterUserRequest("bert", "password", "fake");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsEmailIfOver64Characters() {
            RegisterUserRequest reg = new RegisterUserRequest("bert2", "password", "1234567890123456789012345123456789012345678901234512345678901234@fake.com");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsEmailIfAlreadyTaken() {
            RegisterUserRequest reg = new RegisterUserRequest("bert2", "password", "bert@fake.com");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task AcceptsValidUser() {
            RegisterUserRequest reg = new RegisterUserRequest("bert2", "password", "fake@mail.com");
            var result = await validator.ValidateAsync(reg);
            Assert.IsTrue(result.IsValid);
        }
    }
}
