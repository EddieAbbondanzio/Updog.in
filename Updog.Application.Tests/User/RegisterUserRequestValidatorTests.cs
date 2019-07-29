using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Updog.Application;
using System.Threading.Tasks;

namespace Updog.Application.Tests {
    /// <summary>
    /// Unit tests for the RegisterUserRequestValidator
    /// </summary>
    [TestClass]
    public class RegisterUserRequestValidatorTests {
        Application.RegisterUserValidator validator = new Application.RegisterUserValidator(new MockUserRepo());

        [TestMethod]
        public async Task FailsUsernameIfNull() {
            RegisterUserParams reg = new RegisterUserParams(null, "password");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsUsernameUnder4Characters() {
            RegisterUserParams reg = new RegisterUserParams("cat", "password");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsUsernameOver24Characters() {
            RegisterUserParams reg = new RegisterUserParams("1234567890123456789012345", "password");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsUsernameIfTaken() {
            RegisterUserParams reg = new RegisterUserParams("bert", "password");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsUsernameIfInvalidCharacters() {
            RegisterUserParams reg = new RegisterUserParams("@bert", "password");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsPasswordWhenNull() {
            RegisterUserParams reg = new RegisterUserParams("bert", null);
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsPasswordWhenEmpty() {
            RegisterUserParams reg = new RegisterUserParams("bert", "         ");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsPasswordWhenUnder8Characters() {
            RegisterUserParams reg = new RegisterUserParams("bert", "cat");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsEmailIfNull() {
            RegisterUserParams reg = new RegisterUserParams("bert", "password", null);
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsEmailIfNotNullAndInvalid() {
            RegisterUserParams reg = new RegisterUserParams("bert", "password", "fake");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsEmailIfOver64Characters() {
            RegisterUserParams reg = new RegisterUserParams("bert2", "password", "1234567890123456789012345123456789012345678901234512345678901234@fake.com");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsEmailIfAlreadyTaken() {
            RegisterUserParams reg = new RegisterUserParams("bert2", "password", "bert@fake.com");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task AcceptsValidUser() {
            RegisterUserParams reg = new RegisterUserParams("bert2", "password", "fake@mail.com");
            var result = await validator.ValidateAsync(reg);
            Assert.IsTrue(result.IsValid);
        }
    }
}
