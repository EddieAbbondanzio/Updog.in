import { ApiInteractor } from '@/core/api-interactor';
import { PostInfo } from '@/post/common/post-info';
import { CommentInfo } from '@/comment/common/comment-info';

/**
 * Interactor to find a post by it's ID.
 */
export class CommentFinderByPost extends ApiInteractor<number, CommentInfo[]> {
    public async handle(input: number): Promise<CommentInfo[]> {
        const response = await this.http.get<CommentInfo[]>(`/comment/`, { params: { postId: input } });

        return response.data.map(ci => {
            return new CommentInfo(ci.id, ci.author, ci.body, ci.date);
        });
    }
}
