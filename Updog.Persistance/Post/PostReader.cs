using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Updog.Domain;
using Updog.Domain.Paging;

namespace Updog.Persistance {
    public sealed class PostReader : DatabaseReader<PostReadView>, IPostReader {
        #region Constructor(s)
        public PostReader(IDatabase database) : base(database) { }
        #endregion

        #region Publics
        public async Task<PostReadView?> FindById(int id, User? user = null) {
            var post = await Connection.QueryFirstOrDefaultAsync<PostRecord>(
                @"SELECT * FROM post
                WHERE was_deleted = FALSE
                ORDER BY post.creation_date DESC
                LIMIT @Limit
                OFFSET @Offset",
                new {
                    Id = id
                }
            );

            //Get total count
            int totalCount = await Connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM post;"
            );

            IUserReader userReader = GetReader<IUserReader>();
            ISpaceReader spaceReader = GetReader<ISpaceReader>();

            PostReadView view = Map(post);

            view.User = (await userReader.FindById(post.UserId))!;
            view.Space = (await spaceReader.FindById(post.SpaceId))!;

            return view;
        }

        public async Task<PagedResultSet<PostReadView>> FindByNew(PaginationInfo paging, User? user = null) {
            var posts = await Connection.QueryAsync<PostRecord>(
                @"SELECT * FROM post
                WHERE was_deleted = FALSE
                ORDER BY post.creation_date DESC
                LIMIT @Limit
                OFFSET @Offset",
                new {
                    Limit = paging.PageSize,
                    Offset = paging.Offset
                }
            );

            //Get total count
            int totalCount = await Connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM post;"
            );

            IUserReader userReader = GetReader<IUserReader>();
            ISpaceReader spaceReader = GetReader<ISpaceReader>();


            List<PostReadView> views = new List<PostReadView>();
            foreach (PostRecord post in posts) {
                PostReadView view = Map(post);

                view.User = (await userReader.FindById(post.UserId))!;
                view.Space = (await spaceReader.FindById(post.SpaceId))!;

                views.Add(view);
            }

            return new PagedResultSet<PostReadView>(views, new PaginationInfo(paging.PageNumber, paging.PageSize, totalCount));
        }

        public async Task<PagedResultSet<PostReadView>> FindBySpace(string space, PaginationInfo paging, User? user = null) {
            var posts = await Connection.QueryAsync<PostRecord>(
                @"SELECT post.* FROM post
                    LEFT JOIN space ON space.id = post.space_id
                    WHERE LOWER(space.name) = LOWER(@Name) AND post.was_deleted = FALSE
                    ORDER BY post.creation_date DESC
                    LIMIT @Limit
                    OFFSET @Offset",
                    new {
                        Name = space,
                        Limit = paging.PageSize,
                        Offset = paging.Offset
                    }
            );

            //Get total count
            int totalCount = await Connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM post 
                    LEFT JOIN space ON post.space_id = space.id 
                    WHERE LOWER(space.name) = LOWER(@Name) AND post.was_deleted = FALSE;", new { Name = space }
            );

            IUserReader userReader = GetReader<IUserReader>();
            ISpaceReader spaceReader = GetReader<ISpaceReader>();


            List<PostReadView> views = new List<PostReadView>();
            foreach (PostRecord post in posts) {
                PostReadView view = Map(post);

                view.User = (await userReader.FindById(post.UserId))!;
                view.Space = (await spaceReader.FindById(post.SpaceId))!;

                views.Add(view);
            }

            return new PagedResultSet<PostReadView>(views, new PaginationInfo(paging.PageNumber, paging.PageSize, totalCount));

        }

        public async Task<PagedResultSet<PostReadView>> FindByUser(string username, PaginationInfo paging, User? user = null) {
            var posts = await Connection.QueryAsync<PostRecord>(
                @"SELECT post.* FROM post
                    LEFT JOIN ""user"" u1 ON u1.id = post.user_id
                    WHERE u1.username = @Username AND post.was_deleted = FALSE
                    ORDER BY post.creation_date DESC
                    LIMIT @Limit
                    OFFSET @Offset",
                    new {
                        Username = username,
                        Limit = paging.PageSize,
                        Offset = paging.Offset
                    }
            );

            //Get total count
            int totalCount = await Connection.ExecuteScalarAsync<int>(
                @"SELECT COUNT(*) FROM post 
                    LEFT JOIN ""user"" ON post.user_id = ""user"".id 
                    WHERE ""user"".username = @Username AND post.was_deleted = FALSE",
                new { Username = username }
            );

            IUserReader userReader = GetReader<IUserReader>();
            ISpaceReader spaceReader = GetReader<ISpaceReader>();


            List<PostReadView> views = new List<PostReadView>();
            foreach (PostRecord post in posts) {
                PostReadView view = Map(post);

                view.User = (await userReader.FindById(post.UserId))!;
                view.Space = (await spaceReader.FindById(post.SpaceId))!;

                views.Add(view);
            }

            return new PagedResultSet<PostReadView>(views, new PaginationInfo(paging.PageNumber, paging.PageSize, totalCount));

        }
        #endregion

        #region Privates
        private PostReadView Map(PostRecord source) => new PostReadView() {
            Id = source.Id,
            Type = source.Type,
            Title = source.Title,
            Body = source.Body,
            CreationDate = source.CreationDate,
            WasUpdated = source.WasUpdated,
            WasDeleted = source.WasUpdated,
            Upvotes = source.Upvotes,
            Downvotes = source.Downvotes
        };
        #endregion
    }
}