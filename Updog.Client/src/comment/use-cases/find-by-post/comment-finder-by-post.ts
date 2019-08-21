import { ApiInteractor } from '@/core/api-interactor';
import { Comment } from '@/comment/common/comment';
import { CommentApiInteractor } from '@/comment/common/comment-api-interactor';

/**
 * Interactor to find a post by it's ID.
 */
export class CommentFinderByPost extends CommentApiInteractor<number, Comment[]> {
    public async handle(input: number): Promise<Comment[]> {
        const response = await this.http.get<Comment[]>(`/comment/`, { params: { postId: input } });

        return response.data.map(ci => {
            return this.commentMapper.map(ci);
        });
    }
}
