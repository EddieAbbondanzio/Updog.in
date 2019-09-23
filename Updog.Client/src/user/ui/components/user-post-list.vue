<template>
    <div v-if="$posts != null">
        <post-summary :post="post" v-for="post in $posts" v-bind:key="post.id" />

        <pagination-nav :pagination="$posts.pagination" @previous="onPrevious" @next="onNext" />
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { PostFinderMixin, PostFinderByUserParams } from '../../../post';
import { User } from '../../domain/user';
import PostSummary from '@/post/ui/components/post-summary.vue';
import PaginationNav from '@/core/ui/components/pagination-nav.vue';
import { PaginationParams } from '../../../core';
import { Post } from '@/post';

/**
 * List of posts made by a user
 */
@Component({
    name: 'user-post-list',
    components: {
        PostSummary,
        PaginationNav
    }
})
export default class UserPostList extends PostFinderMixin {
    public static DEFAULT_POST_PAGE_SIZE = 10;

    /**
     * The user to retrieve posts for.
     */
    @Prop()
    public user!: User;

    /**
     * The current page of posts being viewed.
     */
    public currentPage: number = 0;

    public async created() {
        this.refreshPosts();
    }

    public async onNext() {
        if (!this.$posts!.pagination.hasNextPage()) {
            throw new Error('No next page');
        }

        this.currentPage++;
        this.refreshPosts();
    }

    public async onPrevious() {
        if (!this.$posts!.pagination.hasPreviousPage()) {
            throw new Error('No previous page');
        }

        this.currentPage--;
        this.refreshPosts();
    }

    public async refreshPosts() {
        await this.$findPostsByUser(
            new PostFinderByUserParams(
                this.user.username,
                new PaginationParams(this.currentPage, UserPostList.DEFAULT_POST_PAGE_SIZE)
            )
        );
    }
}
</script>