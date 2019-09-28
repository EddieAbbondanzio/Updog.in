<template>
    <div v-if="comments != null">
        <comment-summary-list :comments="comments" />

        <pagination-nav :pagination="comments.pagination" @previous="onPrevious" @next="onNext" />
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { CommentFinderByUserParams } from '../../../comment/interactors/find-by-user/comment-finder-by-user-params';
import { User } from '../../domain/user';
import { Comment } from '@/comment/domain/comment';
import { PagedResultSet } from '@/core/pagination/paged-result-set';
import PaginationNav from '@/core/ui/components/pagination-nav.vue';
import CommentFinderMixin from '@/comment/mixins/comment-finder-mixin';
import { PaginationParams } from '@/core/pagination/pagination-params';
import CommentSummaryList from '@/comment/ui/components/comment-summary-list.vue';

/**
 * List of Comments made by a user
 */
@Component({
    name: 'user-comment-list',
    components: {
        PaginationNav,
        CommentSummaryList
    }
})
export default class UserCommentList extends CommentFinderMixin {
    public static DEFAULT_PAGE_SIZE = 20;

    /**
     * The user to retrieve Comments for.
     */
    @Prop()
    public user!: User;

    /**
     * The current page of Comments being viewed.
     */
    public currentPage: number = 0;

    public comments: PagedResultSet<Comment> | null = null;

    public async created() {
        this.refreshComments();
    }

    public async onNext() {
        if (!this.comments!.pagination.hasNextPage()) {
            throw new Error('No next page');
        }

        this.currentPage++;
        this.refreshComments();
    }

    public async onPrevious() {
        if (!this.comments!.pagination.hasPreviousPage()) {
            throw new Error('No previous page');
        }

        this.currentPage--;
        this.refreshComments();
    }

    public async refreshComments() {
        this.comments = await this.$findCommentsByUser(
            new CommentFinderByUserParams(
                this.user.username,
                new PaginationParams(this.currentPage, UserCommentList.DEFAULT_PAGE_SIZE)
            )
        );
    }
}
</script>