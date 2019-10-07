using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Updog.Domain;
using Updog.Domain.Paging;

namespace Updog.Persistance {
    public sealed class PostReader : DatabaseReader<PostReadView>, IPostReader {
        #region Fields
        private IPostReadViewMapper mapper;
        #endregion

        #region Constructor(s)
        public PostReader(IDatabase database, IPostReadViewMapper mapper) : base(database) {
            this.mapper = mapper;
        }
        #endregion

        #region Publics
        public async Task<PostReadView?> FindById(int id, User? user = null) {
            var post = await Connection.QueryFirstOrDefaultAsync<PostRecord>(
                @"SELECT * FROM Post
                ORDER BY Post.CreationDate DESC
                WHERE WasDeleted = FALSE
                LIMIT @Limit
                OFFSET @Offset",
                new {
                    Id = id
                }
            );

            //Get total count
            int totalCount = await Connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Post;"
            );

            IUserReader userReader = GetReader<IUserReader>();
            ISpaceReader spaceReader = GetReader<ISpaceReader>();

            PostReadView view = mapper.Map(post);

            view.User = (await userReader.FindById(post.UserId))!;
            view.Space = (await spaceReader.FindById(post.SpaceId))!;

            return view;
        }

        public async Task<PagedResultSet<PostReadView>> FindByNew(PaginationInfo paging, User? user = null) {
            var posts = await Connection.QueryAsync<PostRecord>(
                @"SELECT * FROM Post
                ORDER BY Post.CreationDate DESC
                WHERE WasDeleted = FALSE
                LIMIT @Limit
                OFFSET @Offset",
                new {
                    Limit = paging.PageSize,
                    Offset = paging.Offset
                }
            );

            //Get total count
            int totalCount = await Connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM Post;"
            );

            IUserReader userReader = GetReader<IUserReader>();
            ISpaceReader spaceReader = GetReader<ISpaceReader>();


            List<PostReadView> views = new List<PostReadView>();
            foreach (PostRecord post in posts) {
                PostReadView view = mapper.Map(post);

                view.User = (await userReader.FindById(post.UserId))!;
                view.Space = (await spaceReader.FindById(post.SpaceId))!;

                views.Add(view);
            }

            return new PagedResultSet<PostReadView>(views, new PaginationInfo(paging.PageNumber, paging.PageSize, totalCount));
        }

        public async Task<PagedResultSet<PostReadView>> FindBySpace(string space, PaginationInfo paging, User? user = null) {
            var posts = await Connection.QueryAsync<PostRecord>(
                @"SELECT Post.* FROM Post
                    LEFT JOIN Space ON Space.Id = Post.SpaceId
                    WHERE LOWER(Space.Name) = LOWER(@Name) AND Post.WasDeleted = FALSE
                    ORDER BY Post.CreationDate DESC
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
                "SELECT COUNT(*) FROM Post LEFT JOIN Space ON Post.SpaceId = Space.Id WHERE LOWER(Space.Name) = LOWER(@Name) AND Post.WasDeleted = FALSE;", new { Name = space }
            );

            IUserReader userReader = GetReader<IUserReader>();
            ISpaceReader spaceReader = GetReader<ISpaceReader>();


            List<PostReadView> views = new List<PostReadView>();
            foreach (PostRecord post in posts) {
                PostReadView view = mapper.Map(post);

                view.User = (await userReader.FindById(post.UserId))!;
                view.Space = (await spaceReader.FindById(post.SpaceId))!;

                views.Add(view);
            }

            return new PagedResultSet<PostReadView>(views, new PaginationInfo(paging.PageNumber, paging.PageSize, totalCount));

        }

        public async Task<PagedResultSet<PostReadView>> FindByUser(string username, PaginationInfo paging, User? user = null) {
            var posts = await Connection.QueryAsync<PostRecord>(
                @"SELECT Post.* FROM Post
                    LEFT JOIN ""User"" u1 ON u1.Id = Post.UserId
                    WHERE u1.Username = @Username AND Post.WasDeleted = FALSE
                    ORDER BY Post.CreationDate DESC
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
                @"SELECT COUNT(*) FROM Post LEFT JOIN ""User"" ON Post.UserId = ""User"".Id WHERE ""User"".Username = @Username AND Post.WasDeleted = FALSE",
                new { Username = username }
            );

            IUserReader userReader = GetReader<IUserReader>();
            ISpaceReader spaceReader = GetReader<ISpaceReader>();


            List<PostReadView> views = new List<PostReadView>();
            foreach (PostRecord post in posts) {
                PostReadView view = mapper.Map(post);

                view.User = (await userReader.FindById(post.UserId))!;
                view.Space = (await spaceReader.FindById(post.SpaceId))!;

                views.Add(view);
            }

            return new PagedResultSet<PostReadView>(views, new PaginationInfo(paging.PageNumber, paging.PageSize, totalCount));

        }
        #endregion
    }
}