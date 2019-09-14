<template>
    <layout>
        <template>
            <b-tabs>
                <b-tab title="Posts">
                    <div v-if="$posts != null && $posts.length > 0">
                        <post-summary :post="post" v-for="post in $posts" v-bind:key="post.id" />

                        <pagination-nav
                            :pagination="$posts.pagination"
                            @previous="onPostPrevious"
                            @next="onPostNext"
                        />
                    </div>
                </b-tab>
                <b-tab title="Comments">
                    <div v-if="comments != null">
                        <comment-summary
                            :comment="comment"
                            v-for="comment in comments"
                            v-bind:key="comment.id"
                        />

                        <pagination-nav
                            :pagination="comments.pagination"
                            @previous="onCommentPrevious"
                            @next="onCommentNext"
                        />
                    </div>
                </b-tab>
            </b-tabs>
        </template>
        <template slot="side-bar">
            <div class="bg-light border p-3">
                <user-summary :user="user" />
            </div>
        </template>
    </layout>
</template>

<script lang="ts">
import { Component, Vue, Prop, Mixins } from 'vue-property-decorator';
import Layout from '@/core/ui/components/layout.vue';
import { User as UserEntity } from '@/user';
import UserSummary from '@/user/ui/components/user-summary.vue';
import PostSummary from '@/post/ui/components/post-summary.vue';
import CommentSummary from '@/comment/ui/components/comment-summary.vue';
import PaginationNav from '@/core/ui/components/pagination-nav.vue';
import { UserFinderMixin } from '@/user';
import { PagedResultSet, PaginationParams } from '@/core';
import { Post, PostFinderByUserParams, PostFinderMixin } from '@/post';
import { CommentFinderMixin, Comment } from '@/comment';

/**
 * User details page.
 */
@Component({
    name: 'user',
    components: {
        Layout,
        UserSummary,
        PostSummary,
        CommentSummary,
        PaginationNav
    },
    mixins: [UserFinderMixin, PostFinderMixin, CommentFinderMixin]
})
export default class User extends Mixins(UserFinderMixin, PostFinderMixin, CommentFinderMixin) {
    public static DEFAULT_POST_PAGE_SIZE = 20;

    /**
     * The default number of comments loaded per page on the user page.
     */
    public static DEFAULT_COMMENT_PAGE_SIZE = 20;

    /**
     * The ID of the user.
     */
    public user: UserEntity | null = null;

    /**
     * Posts the user has made.
     */
    // public posts: PagedResultSet<Post> | null = null;

    /**
     * The current post page being displayed.
     */
    public postCurrentPage: number = 0;

    /**
     * Comments the user had made.
     */
    public comments: PagedResultSet<Comment> | null = null;

    public commentCurrentpage: number = 0;

    public async created() {
        const username = this.$route.params.username;
        const user = await this.$findUserByUsername(username);

        if (user == null) {
            this.$router.push({ name: 'home' });
            return;
        }

        this.user = user;

        await this.$findPostsByUser(
            new PostFinderByUserParams(
                username,
                new PaginationParams(this.postCurrentPage, User.DEFAULT_POST_PAGE_SIZE)
            )
        );

        this.comments = await this.$findCommentsByUser(
            username,
            new PaginationParams(this.commentCurrentpage, User.DEFAULT_COMMENT_PAGE_SIZE)
        );
    }

    public async onPostNext() {
        this.postCurrentPage++;
        await this.$findPostsByUser(
            new PostFinderByUserParams(
                this.user!.username,
                new PaginationParams(this.postCurrentPage, User.DEFAULT_POST_PAGE_SIZE)
            )
        );
    }

    public async onPostPrevious() {
        this.postCurrentPage--;
        await this.$findPostsByUser(
            new PostFinderByUserParams(
                this.user!.username,
                new PaginationParams(this.postCurrentPage, User.DEFAULT_POST_PAGE_SIZE)
            )
        );
    }

    public async onCommentNext() {
        this.postCurrentPage--;
        this.comments = await this.$findCommentsByUser(
            this.user!.username,
            new PaginationParams(this.commentCurrentpage, User.DEFAULT_COMMENT_PAGE_SIZE)
        );
    }

    public async onCommentPrevious() {
        this.postCurrentPage--;
        this.comments = await this.$findCommentsByUser(
            this.user!.username,
            new PaginationParams(this.commentCurrentpage, User.DEFAULT_COMMENT_PAGE_SIZE)
        );
    }

    public memberLength() {
        return this;
    }
}
</script>