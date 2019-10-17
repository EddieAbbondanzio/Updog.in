using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Updog.Domain;
using Updog.Domain.Paging;

namespace Updog.Persistance {
    public sealed class CommentReader : DatabaseReader<CommentReadView>, ICommentReader {
        #region Constructor(s)
        public CommentReader(IDatabase database) : base(database) { }
        #endregion

        #region Publics
        public async Task<CommentReadView?> FindById(int id, User? user = null) {
            var comments = await Connection.QueryAsync<CommentRecord>(
                @"WITH RECURSIVE commenttree AS (
                    SELECT r.* FROM comment r WHERE id = @Id AND was_deleted = FALSE
                    UNION ALL
                    SELECT c.* FROM comment c
                    INNER JOIN commenttree ct ON ct.id = c.parent_id
                    WHERE c.was_deleted = FALSE
                    ) SELECT * FROM commenttree
                    ORDER BY parent_id, creation_date ASC;",
                new { Id = id }
            );

            IUserReader userReader = GetReader<IUserReader>();
            IVoteReader voteReader = GetReader<IVoteReader>();

            List<CommentReadView> views = new List<CommentReadView>();

            foreach (CommentRecord comment in comments) {
                CommentReadView view = Map(comment);
                view.User = (await userReader.FindById(comment.UserId))!;

                if (user != null) {
                    view.Vote = await voteReader.FindByCommentAndUser(comment.Id, user.Id);
                }

                views.Add(view);
            }

            // We're assuming there will always be one top level comment.
            return BuildCommentTree(views)[0];
        }

        public async Task<IEnumerable<CommentReadView>> FindByPost(int postId, User? user = null) {
            var comments = (await Connection.QueryAsync<CommentRecord>(
                @"WITH RECURSIVE commenttree AS (
                    SELECT r.* FROM comment r WHERE post_id = @PostId AND parent_id = 0 AND was_deleted = FALSE 
                    UNION ALL
                    SELECT c.* FROM comment c
                    INNER JOIN commenttree ct ON ct.id = c.parent_id
                    WHERE c.was_deleted = FALSE
                    ) SELECT * FROM commenttree
                    ORDER BY parent_id, creation_date DESC;",
                new { PostId = postId }
            ));

            IUserReader userReader = GetReader<IUserReader>();
            IVoteReader voteReader = GetReader<IVoteReader>();

            List<CommentReadView> views = new List<CommentReadView>();

            foreach (CommentRecord comment in comments) {
                CommentReadView view = Map(comment);
                view.User = (await userReader.FindById(comment.UserId))!;

                if (user != null) {
                    view.Vote = await voteReader.FindByCommentAndUser(comment.Id, user.Id);
                }

                views.Add(view);
            }

            return BuildCommentTree(views);
        }

        public async Task<PagedResultSet<CommentReadView>> FindByUser(string username, PaginationInfo paging, User? user = null) {
            var comments = await Connection.QueryAsync<CommentRecord>(
                @"SELECT comment.* FROM comment 
                    LEFT JOIN ""user"" ON comment.user_id = ""user"".id 
                    WHERE ""user"".username = @Username AND was_deleted = FALSE 
                    ORDER BY creation_date DESC 
                    LIMIT @Limit 
                    OFFSET @Offset ",
                new {
                    Username = username,
                    Limit = paging.PageNumber,
                    Offset = paging.Offset
                }
            );

            //Get total count
            int totalCount = await Connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM comment 
                    LEFT JOIN ""user"" ON comment.user_id = ""user"".id 
                    WHERE ""user"".username = @Username AND was_deleted = FALSE",
                new { Username = username }
            );

            IUserReader userReader = GetReader<IUserReader>();
            IVoteReader voteReader = GetReader<IVoteReader>();

            List<CommentReadView> views = new List<CommentReadView>();

            foreach (CommentRecord comment in comments) {
                CommentReadView view = Map(comment);
                view.User = (await userReader.FindById(comment.UserId))!;

                if (user != null) {
                    view.Vote = await voteReader.FindByCommentAndUser(comment.Id, user.Id);
                }

                views.Add(view);
            }

            return new PagedResultSet<CommentReadView>(views, paging);
        }
        #endregion


        #region Helpers
        private CommentReadView Map(CommentRecord source) => new CommentReadView() {
            Id = source.Id,
            PostId = source.PostId,
            Body = source.Body,
            CreationDate = source.CreationDate,
            WasUpdated = source.WasUpdated,
            WasDeleted = source.WasDeleted,
            Upvotes = source.Upvotes,
            Downvotes = source.Downvotes
        };

        /// <summary>
        /// Build the comment hierarchy tree from the flat list.
        /// </summary>
        /// <param name="flatComments">The flattened comments.</param>
        /// <returns>The comments in hierarcheal order.</returns>
        private List<CommentReadView> BuildCommentTree(IEnumerable<CommentReadView> flatComments, int rootId = 0) {
            if (flatComments.Count() == 0) {
                return new List<CommentReadView>();
            }

            Dictionary<int, CommentReadView> lookup = new Dictionary<int, CommentReadView>();

            //Populate the lookup table
            foreach (CommentReadView c in flatComments) {
                lookup.Add(c.Id, c);
            }

            //Now iterate through the list and build the tree
            foreach (CommentReadView c in lookup.Values) {
                if (c.Parent != null) {

                    if (lookup.ContainsKey(c.Parent.Id)) {
                        CommentReadView parent = lookup[c.Parent.Id];
                        parent.Children.Add(c);
                    }
                }
            }

            if (rootId != 0) {
                return lookup.Values.Where(c => c.Id == rootId).ToList();
            } else {
                //Pull out the top level list.
                return lookup.Values.Where(c => c.Parent == null).ToList();
            }
        }
        #endregion
    }
}