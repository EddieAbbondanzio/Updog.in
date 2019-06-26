using Blurtle.Application;
using Blurtle.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blurtle.Infrastructure.Tests {
    [TestClass]
    public class JsonWebTokenHandlerTests {
        private JsonWebTokenHandler tokenHandler = new JsonWebTokenHandler(1000, "cat");

        [TestMethod]
        public void CanIssueToken() {
            User user = new User();
            user.Id = 100;

            string token = tokenHandler.IssueToken(user);
            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void CanVerifyToken() {
            User user = new User();
            user.Id = 100;

            string token = tokenHandler.IssueToken(user);
            var result = tokenHandler.ValidateToken(token);

            Assert.AreEqual(result.Status, AuthenticationTokenStatus.Valid);
            Assert.AreEqual(result.UserId, 100);
        }

        [TestMethod]
        public void CanRejectToken() {

            string token = "cat.goes.meow";
            var result = tokenHandler.ValidateToken(token);
            Assert.AreEqual(result.Status, AuthenticationTokenStatus.Invalid);
        }
    }
}