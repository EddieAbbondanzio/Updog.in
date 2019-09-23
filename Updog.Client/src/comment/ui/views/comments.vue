<template>
    <v-card class="mt-6 pa-3" v-if="$comments != null">
        <!-- Header -->
        <div class="mb-5">
            <span class="title">All&nbsp;{{commentCount}}</span>

            <comment-create-form ref="commentCreateForm" @submit="onCommentCreate" />
        </div>

        <comment-summary v-for="comment in $comments" :comment="comment" v-bind:key="comment.id" />
    </v-card>
</template>

<script lang="ts">
import { Component, Vue, Prop, Mixins } from 'vue-property-decorator';
import CommentSummary from '@/comment/ui/components/comment-summary.vue';
import CommentCreateForm from '@/comment/ui/components/comment-create-form.vue';
import { CommentCreatorMixin, CommentFinderMixin, CommentFinderByPostParams, CommentCreateParams } from '@/comment';

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

    get commentCount() {
        if (this.$comments == null) {
            return '0 comments';
        }

        return this.$comments.length === 1 ? '1 comment' : `${this.$comments.length} comments`;
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