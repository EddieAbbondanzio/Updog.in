import Mixin from 'vue-class-component';
import Vue from 'vue';
import { CommentFinderByUserParams } from '../interactors/find-by-user/comment-finder-by-user-params';
import { CommentFinderByPostParams } from '../interactors/find-by-post/comment-finder-by-post-params';
import { getModule } from 'vuex-module-decorators';
import CommentStore from '../store/comment-store';
import { Comment } from '@/comment/domain/comment';
import { PagedResultSet } from '@/core/pagination/paged-result-set';

/**
 * Mixin to handle comment related things.
 */
@Mixin
export default class CommentFinderMixin extends Vue {
    get $comments() {
        const module = getModule(CommentStore, this.$store);
        return module.comments;
    }

    /**
     * Find a post by it's unique ID.
     * @param request The ID of the post to retrieve.
     */
    public async $findCommentById(request: number) {
        const module = getModule(CommentStore, this.$store);
        return module.findById(request);
    }

    /**
     * Find all of the comments for a post.
     * @param params The info of the post to get comments for.
     */
    public async $findCommentsByPost(params: CommentFinderByPostParams) {
        const module = getModule(CommentStore, this.$store);
        return module.findByPost(params);
    }

    /**
     * Find a list of comments made by a user.
     * @param params The info of the user to get comments for.
     */
    public async $findCommentsByUser(params: CommentFinderByUserParams): Promise<PagedResultSet<Comment>> {
        const module = getModule(CommentStore, this.$store);
        return module.findByUser(params);
    }
}
