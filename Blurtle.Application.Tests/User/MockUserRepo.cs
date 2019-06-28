using System.Threading.Tasks;
using Blurtle.Domain;

namespace Blurtle.Application.Tests {
    /// <summary>
    /// Mock user repo for retrieving a fake user from the "database".
    /// </summary>
    public class MockUserRepo : IUserRepo {
#pragma warning disable 1998
        public Task Add(User user) {
            throw new System.NotImplementedException();
        }

        public Task Delete(User user) {
            throw new System.NotImplementedException();
        }

        public Task<User> FindById(int id) {
            throw new System.NotImplementedException();
        }

        public async Task<User> FindByUsername(string username) {
            if (username == "bert") {
                return new User() {
                    Id = 100,
                    Username = "bert",
                    PasswordHash = "hash",
                    Email = "bert@fake.com"
                };
            } else {
                return null;
            }
        }

        public Task Update(User user) {
            throw new System.NotImplementedException();
        }
    }
#pragma warning restore 1998
}