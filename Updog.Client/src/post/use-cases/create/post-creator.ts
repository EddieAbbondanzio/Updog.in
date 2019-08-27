import { ApiInteractor } from '@/core/api-interactor';
import { PostCreateParams } from './post-create-params';
import { Post } from '@/post/common/post';
import { PostMapper } from '@/post/common/post-mapper';
import { UserMapper } from '@/user/common/user-mapper';
import { PostApiInteractor } from '@/post/common/post-api-interactor';

/**
 * Interactor to create a new post.
 */
export class PostCreator extends PostApiInteractor<PostCreateParams, Post> {
    public async handle(input: PostCreateParams): Promise<Post> {
        const response = await this.http.post<Post>('/post/', input);

        return this.postMapper.map(response.data);
    }
}
