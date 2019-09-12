using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to vote on a comment.
    /// </summary>
    public sealed class CommentVoter : IInteractor<VoteOnCommentParams, VoteView> {
        #region Fields
        private IDatabase database;

        private IVoteViewMapper voteViewMapper;
        #endregion

        #region Constructor(s)
        public CommentVoter(IDatabase database, IVoteViewMapper voteViewMapper) {
            this.database = database;
            this.voteViewMapper = voteViewMapper;
        }
        #endregion

        #region Publics
        public async Task<VoteView> Handle(VoteOnCommentParams input) {
            using (var connection = database.GetConnection()) {
                IVoteRepo voteRepo = database.GetRepo<IVoteRepo>(connection);
                ICommentRepo commentRepo = database.GetRepo<ICommentRepo>(connection);
                IUserRepo userRepo = database.GetRepo<IUserRepo>(connection);

                using (var transaction = connection.BeginTransaction()) {
                    Comment comment = (await commentRepo.FindById(input.CommentId))!;
                    Vote? oldVote = await voteRepo.FindByUserAndComment(input.User.Username, input.CommentId);


                    // Wipe out the old one...
                    if (oldVote != null) {
                        comment.RemoveVote(oldVote.Direction);
                        await voteRepo.Delete(oldVote);

                        comment.User.CommentKarma -= (int)oldVote.Direction;
                    }

                    // Create the new vote, and update the comment's karma cache.
                    Vote newVote = new Vote() {
                        User = input.User,
                        ResourceType = VoteResourceType.Comment,
                        ResourceId = input.CommentId,
                        Direction = input.Vote
                    };

                    comment.AddVote(newVote.Direction);

                    comment.User.CommentKarma += (int)newVote.Direction;

                    await voteRepo.Add(newVote);
                    await commentRepo.Update(comment);
                    await userRepo.Update(comment.User);

                    transaction.Commit();
                    return voteViewMapper.Map(newVote);
                }
            }
        }
        #endregion
    }
}