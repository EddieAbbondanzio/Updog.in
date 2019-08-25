import { PostApiInteractor } from '@/post/common/post-api-interactor';
import { PostUpdateParams } from './post-update-params';
import { Post } from '@/post/common/post';
import { Context } from '@/core/context';

/**
 * Interactor to update an existing post.
 */
export class PostUpdater extends PostApiInteractor<PostUpdateParams, Post> {
    public async handle(input: PostUpdateParams): Promise<Post> {
        // Crash hard if not authed. The backend will catch this with a 401 response.
        if (Context.login == null) {
            throw new Error('Not logged in!');
        }

        const response = await this.http.patch<Post>(
            `/post/${input.postId}`,
            { body: input.body },
            {
                headers: { Authorization: `Bearer ${Context.login.authToken}` }
            }
        );

        return this.postMapper.map(response.data);
    }
}
