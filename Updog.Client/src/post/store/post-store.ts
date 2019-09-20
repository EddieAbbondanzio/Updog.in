import { Module, VuexModule, Mutation, Action, getModule } from 'vuex-module-decorators';
import { PostCreateParams } from '../interactors/create/post-create-params';
import { User } from '@/user/domain/user';
import { PostFinderById } from '../interactors/find-by-id/post-finder-by-id';
import { PostFinderByNew } from '../interactors/find-by-new/post-finder-by-new';
import { PaginationInfo } from '@/core/pagination/pagination-info';
import { PostFinderByUser } from '../interactors/find-by-user/post-finder-by-user';
import { PostFinderByUserParams } from '../interactors/find-by-user/post-finder-by-user-params';
import { PostCreator } from '../interactors/create/post-creator';
import UserModule from '@/user/store/user-store';
import { Post } from '../domain/post';
import { PagedResultSet } from '@/core/pagination/paged-result-set';
import { PaginationParams } from '@/core/pagination/pagination-params';
import { PostMutation } from './post-mutation';
import { PostUpdateParams } from '../interactors/update/post-update-params';
import { PostUpdater } from '../interactors/update/post-updater';
import { VoteDirection } from '@/vote/domain/vote-direction';
import { VoteOnPostParams } from '@/vote/interactors/vote-on-post/vote-on-post-params';
import { PostFindBySpaceParams } from '../interactors/find-by-space/post-find-by-space-params';
import { PostFinderBySpace } from '../interactors/find-by-space/post-finder-by-space';
import { StoreNamespace } from '@/core/store/store-namespace';
import { PostAction } from './post-action';

/**
 * Module for posts
 */
@Module({ namespaced: true, name: StoreNamespace.Post })
export default class PostStore extends VuexModule {
    /**
     * The collection of cached posts.
     */
    public posts: PagedResultSet<Post> | null = null;

    get post() {
        return this.posts![0];
    }

    @Mutation
    public [PostMutation.SetPosts](posts: PagedResultSet<Post>) {
        this.posts = posts;
    }

    @Mutation
    public [PostMutation.ClearPosts]() {
        this.posts = null;
    }

    @Mutation
    public [PostMutation.IncrementCommentCount](postId: number) {
        const post = this.posts!.find(p => p.id === postId);

        if (post != null) {
            post.commentCount++;
        }
    }

    @Mutation
    public [PostMutation.Vote](params: VoteOnPostParams) {
        const post = this.posts!.find(p => p.id === params.postId);

        if (post != null) {
            post.applyVote(params.vote);
        }
    }

    @Mutation
    public [PostMutation.ClearVotes]() {
        if (this.posts == null) {
            return;
        }

        for (const p of this.posts) {
            p.vote = null;
        }
    }

    @Mutation
    public [PostMutation.Edit](params: PostUpdateParams) {
        const post = this.posts!.find(p => p.id === params.postId);

        if (post == null) {
            throw new Error(`No post with ${params.postId} to update.`);
        }

        post.body = params.body;
        post.wasUpdated = true;
    }

    /**
     * Create a new post.
     * @param params The post creation parameters.
     */
    @Action({ rawError: true })
    public async [PostAction.Create](params: PostCreateParams) {
        return new PostCreator(this.context.rootGetters['user/authToken']).handle(params);
    }

    /**
     * Update an existing post.
     * @param params The post to update params.
     */
    @Action({ rawError: true })
    public async [PostAction.Update](params: PostUpdateParams) {
        if (this.posts == null) {
            throw new Error('Bad move Brochowski');
        }

        const p = await new PostUpdater(this.context.rootGetters['user/authToken']).handle(params);

        this.context.commit(PostMutation.Edit, params);
        return p;
    }

    /**
     * Find an active post.
     * @param id The post to look for.
     */
    @Action({ rawError: true })
    public async [PostAction.FindById](id: number) {
        const post = await new PostFinderById(this.context.rootGetters['user/authToken']).handle(id);
        this.context.commit(PostMutation.SetPosts, new PagedResultSet([post], new PaginationInfo(0, 1, 1)));

        return post;
    }

    /**
     * Find a set of new posts.
     * @param paging The paging info.
     */
    @Action({ rawError: true })
    public async [PostAction.FindByNew](paging: PaginationParams) {
        this.context.commit(PostMutation.ClearPosts);
        const posts = await new PostFinderByNew(this.context.rootGetters['user/authToken']).handle(paging);
        this.context.commit(PostMutation.SetPosts, posts);

        return posts;
    }

    /**
     * Find a set of posts for a user.
     * @param params The input parameters.
     */
    @Action({ rawError: true })
    public async [PostAction.FindByUser](params: PostFinderByUserParams) {
        this.context.commit(PostMutation.ClearPosts);
        const posts = await new PostFinderByUser(this.context.rootGetters['user/authToken']).handle(params);
        this.context.commit(PostMutation.SetPosts, posts);

        return posts;
    }

    /**
     * Find posts for a space.
     * @param params The input params.
     */
    @Action({ rawError: true })
    public async [PostAction.FindBySpace](params: PostFindBySpaceParams) {
        this.context.commit(PostMutation.ClearPosts);
        const posts = await new PostFinderBySpace(this.context.rootGetters['user/authToken']).handle(params);
        this.context.commit(PostMutation.SetPosts, posts);

        return posts;
    }
}
