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
        public async Task<Comment> Create(CommentCreateData createData, User user) {
            Comment c = factory.Create(createData, user);
            await repo.Add(c);

            await bus.Dispatch(new CommentCreateEvent(c));

            return c;
        }

        public async Task<Comment> Update(CommentUpdateData updateData, User user) {
            Comment? c = await repo.FindById(updateData.CommentId);

            if (c == null) {
                throw new InvalidOperationException();
            }

            c.Body = updateData.Body;
            await repo.Update(c);

            await bus.Dispatch(new CommentUpdateEvent(c));

            return c;
        }

        public async Task<Comment> Delete(CommentDeleteData deleteData, User user) {
            Comment? c = await repo.FindById(deleteData.CommentId);

            if (c == null) {
                throw new InvalidOperationException();
            }

            c.Delete();
            await repo.Update(c);

            await bus.Dispatch(new CommentDeleteEvent(c));

            return c;
        }
        #endregion
    }
}