import { PostApiInteractor } from '@/post/common/post-api-interactor';
import { PostUpdateParams } from './post-update-params';
import { Post } from '@/post/common/post';

/**
 * Interactor to update an existing post.
 */
export class PostUpdater extends PostApiInteractor<PostUpdateParams, Post> {
    public async handle(input: PostUpdateParams): Promise<Post> {
        const response = await this.http.patch<Post>(`/post/${input.postId}`, { body: input.body });

        return this.postMapper.map(response.data);
    }
}
