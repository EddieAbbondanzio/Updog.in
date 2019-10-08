using System;
using System.Threading.Tasks;

namespace Updog.Domain {
    public sealed class PostService : IPostService {
        #region Fields
        private IEventBus bus;
        private IPostFactory factory;
        private IPostRepo repo;
        #endregion

        #region Constructor(s)
        public PostService(IEventBus bus, IPostFactory factory, IPostRepo repo) {
            this.bus = bus;
            this.factory = factory;
            this.repo = repo;
        }
        #endregion

        #region Publics
        public async Task<Post> Create(PostCreateData createData, User user) {
            Post p = factory.Create(createData, user);
            await repo.Add(p);

            await bus.Dispatch(new PostCreateEvent(p));

            return p;
        }

        public async Task<Post> Update(PostUpdateData updateData, User user) {
            Post? p = await repo.FindById(updateData.PostId);

            if (p == null) {
                throw new InvalidOperationException();
            }

            p.Update(updateData);
            await repo.Update(p);

            await bus.Dispatch(new PostUpdateEvent(p));
            return p;
        }

        public async Task<Post> Delete(PostDeleteData deleteData, User user) {
            Post? p = await repo.FindById(deleteData.PostId);

            if (p == null) {
                throw new InvalidOperationException();
            }

            p.Delete();
            await repo.Update(p);
            await bus.Dispatch(new PostDeleteEvent(p));

            return p;
        }
        #endregion
    }
}