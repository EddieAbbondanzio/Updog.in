using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Updog.Application;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application.Tests {
    /// <summary>
    /// Unit tests for the user finder via username.
    /// </summary>
    [TestClass]
    public class UserFinderByUsernameTests {
        private UserFinderByUsername userFinder = new UserFinderByUsername(new MockUserRepo(), new UserViewMapper());

        [TestMethod]
        public async Task ReturnsNullIfNoUserFound() {
            UserView u = await this.userFinder.Handle("null");
            Assert.IsNull(u);
        }

        [TestMethod]
        public async Task ReturnsUserIfFound() {
            UserView u = await this.userFinder.Handle("bert");
            Assert.IsNotNull(u);
        }
    }
}