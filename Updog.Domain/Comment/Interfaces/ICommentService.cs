using System.Threading.Tasks;

namespace Updog.Domain {
    public interface ICommentService : IService<Comment> {
        Task<Comment?> FindById(int id);
        Task<Comment> Create(CommentCreate create, User user);
        Task Update(int commentId, CommentUpdate update, User user);
        Task Delete(int commentId, User user);
        Task<bool> IsOwner(int commentId, string username);
        Task<bool> DoesCommentExist(int id);
    }
}