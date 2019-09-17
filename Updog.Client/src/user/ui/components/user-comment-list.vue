<template>
    <div v-if="comments != null">
        <Comment-summary :comment="comment" v-for="comment in comments" v-bind:key="comment.id" />

        <pagination-nav :pagination="comments.pagination" @previous="onPrevious" @next="onNext" />
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { CommentFinderMixin, CommentFinderByUserParams } from '../../../comment';
import { User } from '../../domain/user';
import CommentSummary from '@/comment/ui/components/comment-summary.vue';
import PaginationNav from '@/core/ui/components/pagination-nav.vue';
import { PaginationParams, PagedResultSet } from '../../../core';
import { Comment } from '@/comment';

/**
 * List of Comments made by a user
 */
@Component({
    name: 'user-comment-list',
    components: {
        CommentSummary,
        PaginationNav
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