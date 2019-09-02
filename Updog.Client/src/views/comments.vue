<template>
    <div>
        <comment-create-form ref="commentCreateForm" @submit="onCommentCreate" />

        <div v-if="$cachedComments != null">
            <comment-summary
                v-for="comment in $cachedComments"
                :comment="comment"
                v-bind:key="comment.id"
            />
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop, Mixins } from 'vue-property-decorator';
import { Comment } from '../comment/common/comment';
import { CommentFinderMixin } from '../comment/mixins/comment-finder-mixin';
import { CommentFinderByPostParams } from '../comment/use-cases/find-by-post/comment-finder-by-post-params';
import CommentSummary from '@/comment/components/comment-summary.vue';
import CommentCreateForm from '@/comment/components/comment-create-form.vue';
import { CommentCreatorMixin } from '../comment/mixins/comment-creator-mixin';
import { CommentCreateParams } from '../comment/use-cases/create/comment-create-params';

/**
 * Comment discussion thread of a post.
 */
@Component({
    name: 'comments',
    components: {
        CommentSummary,
        CommentCreateForm
    },
    mixins: [CommentFinderMixin, CommentCreatorMixin]
})
export default class Comments extends Mixins(CommentFinderMixin, CommentCreatorMixin) {
    get postId() {
        return Number.parseInt(this.$route.params.postId, 10);
    }

    /**
     * On load, pull in the comments.
     */
    public async mounted() {
        await this.$findCommentsByPost(new CommentFinderByPostParams(this.postId));
    }

    /**
     * When a comment is created, send it to the backend.
     */
    public async onCommentCreate(body: string) {
        await this.$createComment(new CommentCreateParams(body, this.postId));
    }
}
</script>