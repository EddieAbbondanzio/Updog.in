<template>
    <master-page>
        <template>
            <div v-if="post != null">
                <post-summary :post="post" expand="true" />
                <comment-create-form @submit="onCommentCreate" ref="commentCreateForm" />

                <!-- Comments! -->
                <comment-summary
                    v-for="comment in comments"
                    :comment="comment"
                    v-bind:key="comment.id"
                />
            </div>
        </template>
        <template slot="side-bar">
            <create-post-buttons />
        </template>
        <template slot="footer">FOOTER!</template>
    </master-page>
</template>

<script lang="ts">
import { Component, Vue, Mixins } from 'vue-property-decorator';
import CreatePostButtons from '@/post/components/create-post-buttons.vue';
import MasterPage from '@/core/components/master-page.vue';
import { PostMixin } from '../post/mixins/post-mixin';
import PostSummary from '@/post/components/post-summary.vue';
import CommentCreateForm from '@/comment/components/comment-create-form.vue';
import { CommentMixin } from '@/comment/mixins/comment-mixin';
import { mixins } from 'vue-class-component/lib/util';
import { CommentCreateParams } from '../comment/use-cases/create/comment-create-params';
import { Post as PostEntity } from '@/post/common/post';
import CommentSummary from '@/comment/components/comment-summary.vue';
import { Comment } from '../comment/common/comment';

/**
 * View a post via it's ID.
 */
@Component({
    components: {
        CreatePostButtons,
        MasterPage,
        PostSummary,
        CommentCreateForm,
        CommentSummary
    },
    mixins: [PostMixin, CommentMixin]
})
export default class Post extends Mixins(PostMixin, CommentMixin) {
    public $refs!: {
        commentCreateForm: CommentCreateForm;
    };

    /**
     * The post being displayed.
     */
    public post: PostEntity | null = null;

    /**
     * Comments on the post.
     */
    public comments: Comment[] = [];

    public async created() {
        const postId = Number.parseInt(this.$route.params.id, 10);
        this.post = await this.$findPostById(postId);
        this.comments = await this.$findCommentsByPost(postId);
    }

    public async onCommentCreate(comment: string) {
        if (this.post == null) {
            throw new Error();
        }

        const c = await this.$createComment(new CommentCreateParams(comment, this.post!.id, 0));
        this.comments.unshift(c);
        this.$refs.commentCreateForm.clear();
    }
}
</script>
