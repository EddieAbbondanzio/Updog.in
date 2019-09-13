<template>
    <div>
        <span>{{ post.karma}} votes</span>
        <div
            :title="`${post.upvoteRatio.toFixed(2)}% upvoted`"
            v-if="post.upvotes + post.downvotes != 0"
        >
            <div class="d-inline-block upvoted" :style="{width: upvoteWidth}">&nbsp;</div>
            <div class="d-inline-block downvoted" :style="{width: downvoteWidth}">&nbsp;</div>
        </div>
        <div class="bg-secondary" title="No votes" v-else>&nbsp;</div>
    </div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Post } from '../../post/domain/post';

/**
 * A fancy bar to show the percentage of votes that are upvotes / downvotes.
 */
@Component({
    name: 'post-vote-bar'
})
export default class PostVoteBar extends Vue {
    /**
     * The post to work with.
     */
    @Prop()
    public post!: Post;

    /**
     * Percentage width for upvote side of bar.
     */
    get upvoteWidth() {
        return `${this.post.upvoteRatio}%`;
    }

    /**
     * Percentage width for downvote side of bar.
     */
    get downvoteWidth() {
        return `${100 - this.post.upvoteRatio}%`;
    }
}
</script>