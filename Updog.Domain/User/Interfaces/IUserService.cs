using System.Threading.Tasks;

namespace Updog.Domain {
    public interface IUserService : IService<User> {
        Task<User> AdminRegisterOrUpdate(IAdminConfig config);
        Task<UserLogin> Login(UserCredentials credentials);
        Task<UserLogin> Register(UserRegistration registration);
        Task<User> Update(string username, UserUpdate data);
        Task<User> UpdatePassword(string username, UserUpdatePassword data);
    }
}