import Component from 'vue-class-component';
import Vue from 'vue';
import { PostCreateParams } from '../use-cases/create/post-create-params';
import { Post } from '../common/post';
import { PostCreator } from '../use-cases/create/post-creator';
import { PostFinderById } from '../use-cases/find-by-id/post-finder-by-id';
import { PostInfo } from '../common/post-info';
import { PaginationInfo } from '@/core/pagination-info';
import { PostFinderByNew } from '../use-cases/find-by-new/post-finder-by-new';

export class PostMixin extends Vue {
    /**
     * Create a new post.
     * @param request The post creation details.
     */
    public async $createPost(request: PostCreateParams): Promise<Post> {
        return new PostCreator().handle(request);
    }

    /**
     * Find a post by it's unique ID.
     * @param request The ID of the post to retrieve.
     */
    public async $findPostById(request: number): Promise<PostInfo> {
        return new PostFinderById().handle(request);
    }

    /**
     * Find a collection of posts by new.
     * @param paginationInfo The pagination info.
     */
    public async $findPostByNew(paginationInfo: PaginationInfo): Promise<PostInfo[]> {
        return new PostFinderByNew().handle(paginationInfo);
    }
}
