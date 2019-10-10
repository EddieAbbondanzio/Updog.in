using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Domain {
    public sealed class CommentService : ICommentService {
        #region Fields
        private IEventBus bus;
        private ICommentFactory factory;
        private ICommentRepo repo;
        #endregion

        #region Constructor(s)
        public CommentService(IEventBus bus, ICommentFactory factory, ICommentRepo repo) {
            this.bus = bus;
            this.factory = factory;
            this.repo = repo;
        }
        #endregion

        #region Publics
        public async Task<Comment> Create(CommentCreate create, User user) {
            Comment c = factory.Create(create, user);
            await repo.Add(c);

            await bus.Dispatch(new CommentCreateEvent(c));

            return c;
        }

        public async Task<Comment> Update(int commentId, CommentUpdate update, User user) {
            Comment? c = await repo.FindById(commentId);

            if (c == null) {
                throw new NotFoundException($"No comment with Id: {commentId} found.");
            }

            c.Update(update);
            await repo.Update(c);

            await bus.Dispatch(new CommentUpdateEvent(c));

            return c;
        }

        public async Task<Comment> Delete(int commentId, User user) {
            Comment? c = await repo.FindById(commentId);

            if (c == null) {
                throw new NotFoundException($"No comment with Id: {commentId} found.");
            }

            c.Delete();
            await repo.Update(c);

            await bus.Dispatch(new CommentDeleteEvent(c));

            return c;
        }
        #endregion
    }
}