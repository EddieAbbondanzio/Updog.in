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
        Application.UserRegisterValidator validator = new Application.UserRegisterValidator(new MockUserRepo());

        [TestMethod]
        public async Task FailsUsernameIfNull() {
            UserRegisterParams reg = new UserRegisterParams(null, "password");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsUsernameUnder4Characters() {
            UserRegisterParams reg = new UserRegisterParams("cat", "password");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsUsernameOver24Characters() {
            UserRegisterParams reg = new UserRegisterParams("1234567890123456789012345", "password");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsUsernameIfTaken() {
            UserRegisterParams reg = new UserRegisterParams("bert", "password");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsUsernameIfInvalidCharacters() {
            UserRegisterParams reg = new UserRegisterParams("@bert", "password");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsPasswordWhenNull() {
            UserRegisterParams reg = new UserRegisterParams("bert", null);
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsPasswordWhenEmpty() {
            UserRegisterParams reg = new UserRegisterParams("bert", "         ");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsPasswordWhenUnder8Characters() {
            UserRegisterParams reg = new UserRegisterParams("bert", "cat");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task PassesEmailIfNull() {
            UserRegisterParams reg = new UserRegisterParams("bert", "password", null);
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsEmailIfNotNullAndInvalid() {
            UserRegisterParams reg = new UserRegisterParams("bert", "password", "fake");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsEmailIfOver64Characters() {
            UserRegisterParams reg = new UserRegisterParams("bert2", "password", "1234567890123456789012345123456789012345678901234512345678901234@fake.com");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task FailsEmailIfAlreadyTaken() {
            UserRegisterParams reg = new UserRegisterParams("bert2", "password", "bert@fake.com");
            var result = await validator.ValidateAsync(reg);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public async Task AcceptsValidUser() {
            UserRegisterParams reg = new UserRegisterParams("bert2", "password", "fake@mail.com");
            var result = await validator.ValidateAsync(reg);
            Assert.IsTrue(result.IsValid);
        }
    }
}
