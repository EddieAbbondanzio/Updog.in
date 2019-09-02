<template>
    <div v-if="comment != null">
        <b-alert variant="warning" show>
            You are viewing a single comment thread.
            <router-link
                :to="{name:'comments', params: {postId: postId}}"
            >View the rest of the conversation.</router-link>
        </b-alert>
        <div>
            <comment-summary :comment="comment" />
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { CommentFinderMixin } from '../comment/mixins/comment-finder-mixin';
import { Comment as CommentEntity } from '@/comment/common/comment';
import CommentSummary from '@/comment/components/comment-summary.vue';

/**
 * Page for permalnking to a comment.
 */
@Component({
    name: 'comment',
    components: {
        CommentSummary
    }
})
export default class Comment extends CommentFinderMixin {
    public comment: CommentEntity | null = null;

    get postId() {
        return Number.parseInt(this.$route.params.postId, 10);
    }

    public async mounted() {
        this.comment = await this.$findCommentById(Number.parseInt(this.$route.params.commentId, 10));
    }
}
</script>