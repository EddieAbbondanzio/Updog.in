import { ApiInteractor } from '@/core/api-interactor';
import { Post } from '../common/post';
import { Context } from '@/core/context';

/**
 * Interactor to find a post by it's ID.
 */
export class PostFindByIdInteractor extends ApiInteractor<number, Post> {
    public async handle(input: number): Promise<Post> {
        const response = await this.http.get<Post>(`/post/${input}`);
        return new Post(response.data.id, response.data.type, response.data.title, response.data.body);
    }
}
