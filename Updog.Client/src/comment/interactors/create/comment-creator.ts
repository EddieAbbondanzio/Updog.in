import { CommentCreateParams } from './comment-create-params';
import { Comment } from '@/comment';
import { CommentApiInteractor } from '@/comment';

/**
 * Interactor to create a new comment.
 */
export class CommentCreator extends CommentApiInteractor<CommentCreateParams, Comment> {
    public async handle(input: CommentCreateParams): Promise<Comment> {
        const response = await this.http.post<Comment>('/comment/', input);
        return this.commentMapper.map(response.data);
    }
}
