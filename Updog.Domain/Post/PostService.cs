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

        public async Task<Post> Update(int postId, PostUpdate update, User user) {
            Post? p = await repo.FindById(postId);

            if (p == null) {
                throw new NotFoundException($"No post with Id {postId} found.");
            }

            p.Update(update);
            await repo.Update(p);

            await bus.Dispatch(new PostUpdateEvent(p));
            return p;
        }

        public async Task<Post> Delete(int postId, User user) {
            Post? p = await repo.FindById(postId);

            if (p == null) {
                throw new NotFoundException($"No post with Id {postId} found.");
            }

            p.Delete();
            await repo.Update(p);
            await bus.Dispatch(new PostDeleteEvent(p));

            return p;
        }
        #endregion
    }
}