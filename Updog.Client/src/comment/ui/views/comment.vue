<template>
    <div v-if="comment != null">
        <v-alert type="warning" show>
            You are viewing a single comment thread.
            <router-link
                :to="{name:'comments', params: {postId: postId}}"
            >View the rest of the conversation.</router-link>
        </v-alert>
        <div>
            <comment-summary :comment="comment" />
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Comment as CommentEntity } from '@/comment/domain/comment';
import CommentFinderMixin from '@/comment/mixins/comment-finder-mixin';
import CommentSummary from '@/comment/ui/components/comment-summary.vue';

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