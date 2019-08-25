<template>
    <master-page>
        <template>
            <b-tabs cont>
                <b-tab title="Posts">
                    <post-summary :post="post" v-for="post in posts" v-bind:key="post.id" />
                </b-tab>
                <b-tab title="Comments">
                    <comment-summary
                        :comment="comment"
                        v-for="comment in comments"
                        v-bind:key="comment.id"
                    />
                </b-tab>
            </b-tabs>
        </template>
        <template slot="side-bar">
            <div class="bg-light border p-3">
                <user-summary :user="user" />
            </div>
        </template>
    </master-page>
</template>

<script lang="ts">
import { Component, Vue, Prop, Mixins } from 'vue-property-decorator';
import { Post } from '../post/common/post';
import { PostMixin } from '../post/mixins/post-mixin';
import { PaginationParams } from '../core/pagination/pagination-params';
import MasterPage from '@/core/components/master-page.vue';
import { UserMixin } from '@/user/mixins/user-mixin';
import { User as UserEntity } from '@/user/common/user';
import UserSummary from '@/user/components/user-summary.vue';
import PostSummary from '@/post/components/post-summary.vue';
import CommentSummary from '@/comment/components/comment-summary.vue';
import { CommentMixin } from '../comment/mixins/comment-mixin';
import { Comment } from '../comment/common/comment';

/**
 * User details page.
 */
@Component({
    name: 'user',
    components: {
        MasterPage,
        UserSummary,
        PostSummary,
        CommentSummary
    },
    mixins: [UserMixin, PostMixin, CommentMixin]
})
export default class User extends Mixins(UserMixin, PostMixin, CommentMixin) {
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
    public posts: Post[] = [];

    /**
     * Comments the user had made.
     */
    public comments: Comment[] = [];

    public async created() {
        const username = this.$route.params.username;
        this.user = await this.$findUserByUsername(username);

        this.posts = await this.$findPostsByUser(username, new PaginationParams(0, User.DEFAULT_POST_PAGE_SIZE));
        this.comments = await this.$findCommentsByUser(
            username,
            new PaginationParams(0, User.DEFAULT_COMMENT_PAGE_SIZE)
        );
    }

    public memberLength() {
        return this;
    }
}
</script>