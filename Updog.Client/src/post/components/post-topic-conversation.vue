<template>
    <div v-if="post != null">
        <div
            class="bg-light border my-1"
            v-for="comment in comments"
            v-bind:key="comment.id"
        >{{comment.body}}</div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { PostInfo } from '../common/post-info';
import { DateUtils } from '@/core/utils/date-utils';
import { CommentMixin } from '@/comment/mixins/comment-mixin';
import { CommentInfo } from '../../comment/common/comment-info';

@Component({
    name: 'post-topic-header'
})
export default class PostTopicConversation extends CommentMixin {
    /**
     * The post to show.
     */
    @Prop()
    public post!: PostInfo;

    /**
     * The comments of the post.
     */
    public comments: CommentInfo[] = [];

    public async created() {
        this.comments = await this.$findCommentsByPost(this.post.id);
    }
}
</script>