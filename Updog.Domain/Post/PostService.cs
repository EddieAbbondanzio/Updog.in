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
        public async Task<bool> IsOwner(int postId, string username) => await repo.IsOwner(postId, username);

        public async Task<Post> Create(PostCreate createData, Space space, User user) {
            Post p = factory.Create(createData, space, user);
            await repo.Add(p);

            await bus.Dispatch(new PostCreateEvent(p));

            return p;
        }

        public async Task Update(int postId, PostUpdate update, User user) {
            Post? p = await repo.FindById(postId);

            if (p == null) {
                throw new NotFoundException($"No post with Id {postId} found.");
            }

            p.Update(update);
            await repo.Update(p);

            await bus.Dispatch(new PostUpdateEvent(p));
        }

        public async Task Delete(int postId, User user) {
            Post? p = await repo.FindById(postId);

            if (p == null) {
                throw new NotFoundException($"No post with Id {postId} found.");
            }

            p.Delete();
            await repo.Update(p);
            await bus.Dispatch(new PostDeleteEvent(p));
        }

        public async Task<bool> DoesPostExist(int postId) => await repo.Exists(postId);
        #endregion
    }
}