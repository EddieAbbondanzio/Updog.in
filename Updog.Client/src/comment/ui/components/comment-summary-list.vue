<template>
    <div v-if="comments != null">
        <div v-if="comments.length > 0">
            <comment-summary v-for="comment in comments" :comment="comment" :key="comment.id" />
        </div>
        <div v-else class="error--text">There's nothing here!</div>
    </div>
    <div v-else>
        <comment-loading-place-holder v-for="i in 4" :key="i" />
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import CommentSummary from '@/comment/ui/components/comment-summary.vue';
import CommentLoadingPlaceHolder from '@/comment/ui/components/comment-loading-place-holder.vue';
import { Comment } from '../../domain/comment';

@Component({
    name: 'post-summary-list',
    components: {
        CommentSummary,
        CommentLoadingPlaceHolder
    }
})
export default class CommentSummaryList extends Vue {
    @Prop()
    public comments!: Comment[] | null;
}
</script>