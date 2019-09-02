<template>
    <layout>
        <template>
            <div v-if="post != null">
                <post-summary :post="post" :showEditControls="true" expand="true" />

                <!-- Comments! -->
                <router-view></router-view>
            </div>
        </template>
        <template slot="side-bar">
            <create-post-buttons />
        </template>
        <template slot="footer">FOOTER!</template>
    </layout>
</template>

<script lang="ts">
import { Component, Vue, Mixins } from 'vue-property-decorator';
import CreatePostButtons from '@/post/components/create-post-buttons.vue';
import Layout from '@/core/components/layout.vue';
import { PostFinderMixin } from '../post/mixins/post-finder-mixin';
import PostSummary from '@/post/components/post-summary.vue';
import { CommentFinderMixin } from '@/comment/mixins/comment-finder-mixin';
import { CommentCreatorMixin } from '@/comment/mixins/comment-creator-mixin';
import { mixins } from 'vue-class-component/lib/util';
import { Post as PostEntity } from '@/post/common/post';
import CommentSummary from '@/comment/components/comment-summary.vue';
import { Comment } from '../comment/common/comment';
import User from './user.vue';
import { UserAuthMixin } from '@/user/mixins/user-auth-mixin';
import { CommentCreateParams } from '../comment/use-cases/create/comment-create-params';
import { CommentFinderByPostParams } from '../comment/use-cases/find-by-post/comment-finder-by-post-params';
import { PaginationParams } from '../core/pagination/pagination-params';
import { PagedResultSet } from '../core/pagination/paged-result-set';
import PaginationNavigation from '@/core/components/pagination-navigation.vue';

/**
 * View a post via it's ID.
 */
@Component({
    components: {
        CreatePostButtons,
        Layout,
        PostSummary,
        CommentSummary,
        PaginationNavigation
    },
    mixins: [UserAuthMixin, PostFinderMixin, CommentFinderMixin]
})
export default class Post extends Mixins(UserAuthMixin, PostFinderMixin, CommentFinderMixin) {
    /**
     * The post being displayed.
     */
    public post: PostEntity | null = null;

    public async created() {
        const postId = Number.parseInt(this.$route.params.postId, 10);
        this.post = await this.$findPostById(postId);
    }

    /**
     * Event handler for when a comment is created.
     */
    public async onCommentCreate(comment: string) {
        // Redirect to login if no user present.
        if (!this.$isLoggedIn()) {
            this.$redirectToLogin();
            return;
        }

        if (this.post == null) {
            throw new Error();
        }

        // const c = await this.$createComment(new CommentCreateParams(comment, this.post!.id, 0));
        // this.$refs.commentCreateForm.clear();

        // Hack...
        // this.post.commentCount++;
    }
}
</script>
