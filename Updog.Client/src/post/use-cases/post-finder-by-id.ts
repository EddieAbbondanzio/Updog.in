import { ApiInteractor } from '@/core/api-interactor';
import { Post } from '../common/post';
import { Context } from '@/core/context';
import { PostInfo } from '../common/post-info';

/**
 * Interactor to find a post by it's ID.
 */
export class PostFinderById extends ApiInteractor<number, PostInfo> {
    public async handle(input: number): Promise<PostInfo> {
        const response = await this.http.get<PostInfo>(`/post/${input}`);
        return new PostInfo(
            response.data.id,
            response.data.type,
            response.data.title,
            response.data.body,
            response.data.author,
            response.data.date
        );
    }
}
