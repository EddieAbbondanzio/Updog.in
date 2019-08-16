import { ApiInteractor } from '@/core/api-interactor';
import { PostInfo } from '@/post/common/post-info';
import { CommentInfo } from '@/comment/common/comment-info';

/**
 * Interactor to find a post by it's ID.
 */
export class CommentFinderById extends ApiInteractor<number, CommentInfo> {
    public async handle(input: number): Promise<CommentInfo> {
        const response = await this.http.get<CommentInfo>(`/comment/${input}`);

        return new CommentInfo(response.data.id, response.data.author, response.data.body, response.data.date);
    }
}
