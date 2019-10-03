using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostFindBySpaceQueryHandler : QueryHandler<PostFindBySpaceQuery> {
        #region Fields
        private IPostViewMapper postMapper;
        #endregion

        #region Constructor(s)
        public PostFindBySpaceQueryHandler(IDatabase database, IPostViewMapper postMapper) : base(database) {
            this.postMapper = postMapper;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<PostFindBySpaceQuery> context) {
            IPostRepo postRepo = context.Database.GetRepo<IPostRepo>();
            IVoteRepo voteRepo = context.Database.GetRepo<IVoteRepo>();

            PagedResultSet<Post> posts = await postRepo.FindBySpace(context.Input.Space, context.Input.Paging?.PageNumber ?? 0, context.Input.Paging?.PageSize ?? Post.PageSize);

            if (context.Input.User != null) {
                foreach (Post p in posts) {
                    p.Vote = await voteRepo.FindByUserAndPost(context.Input.User.Username, p.Id);
                }
            }

            context.Output.Success(new PagedResultSet<PostView>(posts.Items.Select(p => postMapper.Map(p)), posts.Pagination));
        }
        #endregion
    }
}