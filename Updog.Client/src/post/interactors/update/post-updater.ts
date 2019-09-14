import { PostApiInteractor } from '@/post/infrastructure/post-api-interactor';
import { PostUpdateParams } from './post-update-params';
import { Post } from '@/post/domain/post';

/**
 * Interactor to update an existing post.
 */
export class PostUpdater extends PostApiInteractor<PostUpdateParams, Post> {
    public async handle(input: PostUpdateParams): Promise<Post> {
        const response = await this.http.patch<Post>(`/post/${input.postId}`, { body: input.body });

        return this.postMapper.map(response.data);
    }
}
