<template>
    <div>
        <comment-create-form ref="commentCreateForm" />

        <div v-if="comments != null">
            <comment-summary
                v-for="comment in comments"
                :comment="comment"
                v-bind:key="comment.id"
            />
        </div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Comment } from '../comment/common/comment';
import { CommentFinderMixin } from '../comment/mixins/comment-finder-mixin';
import { CommentFinderByPostParams } from '../comment/use-cases/find-by-post/comment-finder-by-post-params';
import CommentSummary from '@/comment/components/comment-summary.vue';
import CommentCreateForm from '@/comment/components/comment-create-form.vue';

@Component({
    name: 'comments',
    components: {
        CommentSummary,
        CommentCreateForm
    }
})
export default class Comments extends CommentFinderMixin {
    public $refs!: {
        commentCreateForm: CommentCreateForm;
    };

    public comments: Comment[] | null = null;

    public async mounted() {
        this.comments = await this.$findCommentsByPost(
            new CommentFinderByPostParams(Number.parseInt(this.$route.params.postId, 10))
        );
    }
}
</script>