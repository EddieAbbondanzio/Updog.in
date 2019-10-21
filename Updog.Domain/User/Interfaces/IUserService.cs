using System.Threading.Tasks;

namespace Updog.Domain {
    public interface IUserService : IService<User> {
        Task AdminRegisterOrUpdate(IAdminConfig config);
        Task<UserLogin?> Login(UserCredentials credentials);
        Task<UserLogin> Register(UserRegistration registration);
        Task Update(string username, UserUpdate data);
        Task UpdatePassword(string username, UserUpdatePassword data);
        Task<bool> DoesUserExist(string username);
        Task<bool> IsUsernameAvailable(string username);
        Task<bool> IsEmailAlreadyInUse(string email);
    }
}