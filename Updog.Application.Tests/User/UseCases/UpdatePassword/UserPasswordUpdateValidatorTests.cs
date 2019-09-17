// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System;
// using Updog.Application;
// using System.Threading.Tasks;
// using Updog.Domain;

// namespace Updog.Application.Tests {
//     /// <summary>
//     /// Unit tests for the update user password use case.
//     /// </summary>
//     [TestClass]
//     public class UserPasswordUpdateValidatorTests {
//         private User user = new User() { Id = 1, Username = "bert" };

//         private UserPasswordUpdateValidator validator = new UserPasswordUpdateValidator();

//         [TestMethod]
//         public async Task FailsIfPasswordIsNull() {
//             var result = await validator.ValidateAsync(new UserPasswordUpdateParams(user, null!, null!));
//             Assert.IsFalse(result.IsValid);
//         }

//         [TestMethod]
//         public async Task FailsIfPasswordIsEmpty() {
//             var result = await validator.ValidateAsync(new UserPasswordUpdateParams(user, "", ""));
//             Assert.IsFalse(result.IsValid);
//         }

//         [TestMethod]
//         public async Task FailsIfPasswordIsWhiteSpace() {
//             var result = await validator.ValidateAsync(new UserPasswordUpdateParams(user, "", "         "));
//             Assert.IsFalse(result.IsValid);
//         }


//         [TestMethod]
//         public async Task FailsIfPasswordIsBelowMinLength() {
//             var result = await validator.ValidateAsync(new UserPasswordUpdateParams(user, "", "1"));
//             Assert.IsFalse(result.IsValid);
//         }

//         [TestMethod]
//         public async Task PassesValidPassword() {
//             var result = await validator.ValidateAsync(new UserPasswordUpdateParams(user, "", "password2"));
//             Assert.IsTrue(result.IsValid);
//         }
//     }
// }