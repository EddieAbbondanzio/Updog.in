using System.Threading.Tasks;

namespace Updog.Domain {
    public interface IPostService : IService<Post> {
        Task<Post> Create(PostCreate createData, Space space, User user);
        Task<Post> Update(int postId, PostUpdate update, User user);
        Task<Post> Delete(int postId, User user);
    }
}