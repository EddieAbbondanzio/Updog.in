using System.Threading.Tasks;

namespace Updog.Domain {
    public interface ICommentService : IService<Comment> {
        Task<Comment> Create(CommentCreateData createData, User user);
        Task<Comment> Update(CommentUpdateData updateData, User user);
        Task<Comment> Delete(CommentDeleteData deleteData, User user);
    }
}