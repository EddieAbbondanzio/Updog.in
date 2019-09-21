import { PostApiInteractor } from '@/post/infrastructure/post-api-interactor';
import { Post } from '@/post/domain/post';

/**
 * Interactor to de;ete an existing post.
 */
export class PostDeleter extends PostApiInteractor<Post, Post> {
    public async handle(input: Post): Promise<Post> {
        const response = await this.http.delete<Post>(`/post/${input.id}`);

        return this.postMapper.map(response.data);
    }
}
