import { ApiInteractor } from '@/core/api-interactor';
import { Post } from '../../domain/post';
import { PostApiInteractor } from '@/post/infrastructure/post-api-interactor';

/**
 * Interactor to find a post by it's ID.
 */
export class PostFinderById extends PostApiInteractor<number, Post> {
    public async handle(input: number): Promise<Post> {
        const response = await this.http.get<Post>(`/post/${input}`);
        return this.postMapper.map(response.data);
    }
}
