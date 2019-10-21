using System.Threading.Tasks;

namespace Updog.Domain {
    public interface IPostService : IService<Post> {
        Task<Post> Create(PostCreate createData, Space space, User user);
        Task Update(int postId, PostUpdate update, User user);
        Task Delete(int postId, User user);
        Task<bool> IsOwner(int postId, string username);
        Task<bool> DoesPostExist(int postId);
    }
}