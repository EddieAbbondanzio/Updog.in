import { Comment } from '@/comment/domain/comment';
import { CommentApiInteractor } from '@/comment/infrastructure/comment-api-interactor';

/**
 * Interactor to de;ete an existing Comment.
 */
export class CommentDeleter extends CommentApiInteractor<Comment, Comment> {
    public async handle(input: Comment): Promise<Comment> {
        const response = await this.http.delete<Comment>(`/Comment/${input.id}`);

        return this.commentMapper.map(response.data);
    }
}
