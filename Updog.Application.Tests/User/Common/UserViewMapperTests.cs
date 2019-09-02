using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Updog.Application;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application.Tests {
    /// <summary>
    /// Unit tests for the UserViewMapper.
    /// </summary>
    [TestClass]
    public class UserViewMapperTests {
        private UserViewMapper mapper = new UserViewMapper();

        private User user = new User() { Id = 1, Username = "bert", JoinedDate = DateTime.UtcNow };

        [TestMethod]
        public void MapsId() {
            UserView view = mapper.Map(user);
            Assert.AreEqual(view.Id, user.Id);
        }

        [TestMethod]
        public void MapsUsername() {
            UserView view = mapper.Map(user);
            Assert.AreEqual(view.Username, user.Username);
        }

        [TestMethod]
        public void MapsJoinedDate() {
            UserView view = mapper.Map(user);
            Assert.AreEqual(view.JoinedDate, user.JoinedDate);
        }
    }
}