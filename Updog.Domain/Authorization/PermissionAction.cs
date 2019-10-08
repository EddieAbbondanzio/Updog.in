namespace Updog.Application {
    /// <summary>
    /// Action being performed on a resource.
    /// </summary>
    public enum PermissionAction {
        CreatePost,
        ReadPost,
        UpdatePost,
        DeletePost,
        CreateComment,
        ReadComment,
        UpdateComment,
        DeleteComment,
        CreateSpace,
        ReadSpace,
        UpdateSpace,
        DeleteSpace,
    }
}