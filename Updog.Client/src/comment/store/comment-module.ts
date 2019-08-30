import { Module, VuexModule, Mutation, Action, getModule } from 'vuex-module-decorators';
import { CommentCreateParams } from '../use-cases/create/comment-create-params';
import { CommentFinderByUserParams } from '../use-cases/find-by-user/comment-finder-by-user-param';
import { CommentFinderById } from '../use-cases/find-by-id/comment-finder-by-id';
import { CommentFinderByPost } from '../use-cases/find-by-post/comment-finder-by-post';
import { CommentFinderByUser } from '../use-cases/find-by-user/comment-finder-by-user';
import { CommentCreator } from '../use-cases/create/comment-creator';
import { PagedResultSet } from '@/core/pagination/paged-result-set';
import { CommentMutation } from './comment-mutation';
import { PaginationInfo } from '@/core/pagination/pagination-info';
import { Comment } from '@/comment/common/comment';

/**
 * Cache module for comments.
 */
@Module({ namespaced: true, name: 'comment' })
export default class CommentModule extends VuexModule {
    public comments: PagedResultSet<Comment> | null = null;

    public activeComment: Comment | null = null;

    @Mutation
    public [CommentMutation.CacheComments](comments: PagedResultSet<Comment>) {
        this.comments = comments;
    }

    /**
     * Find a comment via it's ID.
     * @param id The comment's ID.
     */
    @Action
    public async findById(id: number) {
        const c = await new CommentFinderById(this.context.rootGetters['user/authToken']).handle(id);
        this.context.commit(CommentMutation.SetActiveComment, c);
    }

    /**
     * Find a set of comments for a post.
     * @param postId The post Id to look for.
     */
    @Action
    public async findByPost(postId: number) {
        const comments = await new CommentFinderByPost(this.context.rootGetters['user/authToken']).handle(postId);
        this.context.commit(CommentMutation.CacheComments, comments);
    }

    /**
     * Find a set comments for a specific user.
     * @param params Finder params..
     */
    @Action
    public async findByUser(params: CommentFinderByUserParams) {
        const comments = await new CommentFinderByUser(this.context.rootGetters['user/authToken']).handle(params);
        this.context.commit(CommentMutation.CacheComments, comments);
    }

    /**
     * Create a new comemnt.
     * @param params The comment creation params.
     */
    @Action
    public async create(params: CommentCreateParams) {
        const c = await new CommentCreator(this.context.rootGetters['user/authToken']).handle(params);
        this.context.commit(CommentMutation.SetActiveComment, c);
    }
}
