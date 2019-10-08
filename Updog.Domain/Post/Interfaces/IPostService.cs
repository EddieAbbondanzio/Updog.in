using System.Threading.Tasks;

namespace Updog.Domain {
    public interface IPostService : IService<Post> {
        Task<Post> Create(PostCreateData createData, User user);
        Task<Post> Update(PostUpdateData updateData, User user);
        Task<Post> Delete(PostDeleteData deleteData, User user);
    }
}