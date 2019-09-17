<template>
    <layout>
        <template v-if="user != null">
            <b-tabs content-class="p-3">
                <b-tab title="Posts">
                    <user-post-list :user="user" />
                </b-tab>
                <b-tab title="Comments">
                    <user-comment-list :user="user" />
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
import CommentSummary from '@/comment/ui/components/comment-summary.vue';
import PaginationNav from '@/core/ui/components/pagination-nav.vue';
import { UserFinderMixin } from '@/user';
import { PagedResultSet, PaginationParams } from '@/core';
import { Post, PostFinderByUserParams, PostFinderMixin } from '@/post';
import { CommentFinderMixin, Comment } from '@/comment';
import UserPostList from '@/user/ui/components/user-post-list.vue';
import UserCommentList from '@/user/ui/components/user-comment-list.vue';

/**
 * User details page.
 */
@Component({
    name: 'user',
    components: {
        Layout,
        UserSummary,
        CommentSummary,
        PaginationNav,
        UserPostList,
        UserCommentList
    }
})
export default class User extends UserFinderMixin {
    /**
     * The ID of the user.
     */
    public user: UserEntity | null = null;

    public async created() {
        const username = this.$route.params.username;
        const user = await this.$findUserByUsername(username);

        if (user == null) {
            this.$router.push({ name: 'home' });
            return;
        }

        this.user = user;
    }
}
</script>