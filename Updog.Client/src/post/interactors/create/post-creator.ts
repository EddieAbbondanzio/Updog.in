import { ApiInteractor } from '@/core/interactors/api-interactor';
import { PostCreateParams } from './post-create-params';
import { Post } from '@/post/domain/post';
import { PostMapper } from '@/post/infrastructure/post-mapper';
import { UserMapper } from '@/user/infrastructure/user-mapper';
import { PostApiInteractor } from '@/post/infrastructure/post-api-interactor';

/**
 * Interactor to create a new post.
 */
export class PostCreator extends PostApiInteractor<PostCreateParams, Post> {
    public async handle(input: PostCreateParams): Promise<Post> {
        const response = await this.http.post<Post>('/post/', input);

        return this.postMapper.map(response.data);
    }
}
