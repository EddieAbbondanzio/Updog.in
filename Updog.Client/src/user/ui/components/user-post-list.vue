<template>
    <div v-if="$posts != null">
        <post-summary-list :posts="$posts" />
        <pagination-nav :pagination="$posts.pagination" @previous="onPrevious" @next="onNext" />
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { User } from '../../domain/user';
import PostFinderMixin from '@/post/mixins/post-finder-mixin';
import PostSummaryList from '@/post/ui/components/post-summary-list.vue';
import PaginationNav from '@/core/ui/components/pagination-nav.vue';
import { PostFinderByUserParams } from '@/post/interactors/find-by-user/post-finder-by-user-params';
import { PaginationParams } from '@/core/pagination/pagination-params';

/**
 * List of posts made by a user
 */
@Component({
    name: 'user-post-list',
    components: {
        PostSummaryList,
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