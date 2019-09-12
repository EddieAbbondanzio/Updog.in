using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to vote on a post.
    /// </summary>
    public sealed class PostVoter : IInteractor<VoteOnPostParams, VoteView> {
        #region Fields
        private IDatabase database;

        private IVoteViewMapper voteViewMapper;
        #endregion

        #region Constructor(s)
        public PostVoter(IDatabase database, IVoteViewMapper voteViewMapper) {
            this.database = database;
            this.voteViewMapper = voteViewMapper;
        }
        #endregion

        #region Publics
        public async Task<VoteView> Handle(VoteOnPostParams input) {
            using (var connection = database.GetConnection()) {
                IVoteRepo voteRepo = database.GetRepo<IVoteRepo>(connection);
                IPostRepo postRepo = database.GetRepo<IPostRepo>(connection);
                IUserRepo userRepo = database.GetRepo<IUserRepo>(connection);

                using (var transaction = connection.BeginTransaction()) {
                    Post post = (await postRepo.FindById(input.PostId))!;
                    Vote? oldVote = await voteRepo.FindByUserAndPost(input.User.Username, input.PostId);


                    // Wipe out the old one...
                    if (oldVote != null) {
                        post.RemoveVote(oldVote.Direction);
                        await voteRepo.Delete(oldVote);

                        if (post.Type != PostType.Text) {
                            post.User.PostKarma -= (int)oldVote.Direction;
                        }
                    }

                    // Create the new vote, and update the comment's karma cache.
                    Vote newVote = new Vote() {
                        User = input.User,
                        ResourceType = VoteResourceType.Post,
                        ResourceId = input.PostId,
                        Direction = input.Vote
                    };

                    post.AddVote(newVote.Direction);

                    if (post.Type != PostType.Text) {
                        post.User.PostKarma += (int)newVote.Direction;
                    }

                    await voteRepo.Add(newVote);
                    await postRepo.Update(post);
                    await userRepo.Update(post.User);

                    transaction.Commit();
                    return voteViewMapper.Map(newVote);
                }
            }
        }
        #endregion
    }
}