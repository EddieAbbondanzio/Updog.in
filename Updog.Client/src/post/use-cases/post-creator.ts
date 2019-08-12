import { ApiInteractor } from '@/core/api-interactor';
import { PostCreateParams } from '../common/post-create-params';
import { Post } from '../common/post';
import { Context } from '@/core/context';

/**
 * Interactor to create a new post.
 */
export class PostCreator extends ApiInteractor<PostCreateParams, Post> {
    public async handle(input: PostCreateParams): Promise<Post> {
        // Crash hard if not authed. The backend will catch this with a 401 response.
        if (Context.login == null) {
            throw new Error('Not logged in!');
        }

        const response = await this.http.post<Post>('/post/', input, {
            headers: { Authorization: `Bearer ${Context.login.authToken}` }
        });
        return new Post(response.data.id, response.data.type, response.data.title, response.data.body);
    }
}
