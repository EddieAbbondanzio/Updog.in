using System;
using System.Threading.Tasks;
using FluentValidation;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to create a new comment.
    /// </summary>
    public sealed class CommentCreator : IInteractor<CommentCreateParams, CommentView> {
        #region Fields
        private ICommentRepo commentRepo;

        private AbstractValidator<CommentCreateParams> commentValidator;

        /// <summary>
        /// Mapper to convert the comment into its DTO.
        /// </summary>
        private IMapper<Comment, CommentView> commentMapper;

        /// <summary>
        /// CRUD interface for posts.
        /// </summary>
        private IPostRepo postRepo;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new comment creator.
        /// </summary>
        /// <param name="commentRepo">The CRUD interface for the DB.</param>
        /// <param name="commentValidator">Validator for verifying comments are good.</param>
        /// <param name="commentMapper">The mapper to build DTOs.</param>
        public CommentCreator(ICommentRepo commentRepo, AbstractValidator<CommentCreateParams> commentValidator, IMapper<Comment, CommentView> commentMapper, IPostRepo postRepo) {
            this.commentRepo = commentRepo;
            this.commentValidator = commentValidator;
            this.commentMapper = commentMapper;
            this.postRepo = postRepo;
        }
        #endregion

        #region Publics
        public async Task<CommentView> Handle(CommentCreateParams input) {
            await commentValidator.ValidateAndThrowAsync(input);
            Comment parent = input.ParentId != 0 ? (await commentRepo.FindById(input.ParentId)) : null;

            Comment comment = new Comment() {
                User = input.User,
                Parent = parent,
                Body = input.Body,
                CreationDate = DateTime.UtcNow
            };

            await commentRepo.Add(comment);
            return commentMapper.Map(comment);
        }
        #endregion
    }
}