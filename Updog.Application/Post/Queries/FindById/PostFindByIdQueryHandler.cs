using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    public sealed class PostFindByIdQueryHandler : QueryHandler<PostFindByIdQuery> {
        #region Fields
        private IPostViewMapper postMapper;
        #endregion

        #region Constructor(s)
        public PostFindByIdQueryHandler(IDatabase database, IPostViewMapper postMapper) : base(database) {
            this.postMapper = postMapper;
        }
        #endregion

        #region Publics
        protected async override Task ExecuteQuery(ExecutionContext<PostFindByIdQuery> context) {
            IPostRepo postRepo = context.Database.GetRepo<IPostRepo>();
            IVoteRepo voteRepo = context.Database.GetRepo<IVoteRepo>();

            Post? post = await postRepo.FindById(context.Input.PostId);

            if (post == null) {
                context.Output.NotFound();
                return;
            }

            //Pull in the vote if needed.
            if (context.Input.User != null) {
                post.Vote = await voteRepo.FindByUserAndPost(context.Input.User.Username, context.Input.PostId);
            }

            context.Output.Success(postMapper.Map(post));
        }
        #endregion
    }
}