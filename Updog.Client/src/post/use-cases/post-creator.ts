import { ApiInteractor } from '@/core/api-interactor';
import { PostCreateRequest } from '../common/post-create-request';
import { Post } from '../common/post';

/**
 * Interactor to create a new post.
 */
export class PostCreator extends ApiInteractor<PostCreateRequest, Post> {
    public async handle(input: PostCreateRequest): Promise<Post> {
        const response = await this.http.post<Post>('/post/', input);
        return new Post(response.data.id, response.data.type, response.data.title, response.data.body);
    }
}
