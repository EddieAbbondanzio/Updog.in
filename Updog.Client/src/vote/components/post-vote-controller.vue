<template>
    <div class="d-flex flex-column justify-content-center">
        <b-button variant="link" @click="upvote()" class="p-0 m-0 vote-arrow">
            <material-icon icon="keyboard_arrow_up" variant="muted" :class="{ upvoted: isUpvoted}" />
        </b-button>
        <div
            class="d-flex flex-row justify-content-center text-center karma-count font-weight-bold"
        >
            <span :class="{ upvoted: isUpvoted, downvoted: isDownvoted}">{{ karma }}</span>
        </div>
        <b-button variant="link" @click="downvote()" class="p-0 m-0 vote-arrow">
            <material-icon
                icon="keyboard_arrow_down"
                variant="muted"
                :class="{downvoted: isDownvoted}"
            />
        </b-button>
    </div>
</template>

<style scoped>
.karma-count {
    font-size: 1em;
}

.vote-arrow {
    height: 32px;
}

.vote-arrow i {
    font-size: 32px;
}
</style>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import MaterialIcon from '@/core/components/material-icon.vue';
import { VoteDirection } from '../domain/vote-direction';
import { Vote } from '../domain/vote';
import { Post } from '../../post/domain/post';
import { PostVoterMixin } from '@/vote/mixins/post-voter-mixin';
import { NumberUtils } from '@/core/utils/number-utils';

/**
 * Control to upvote, or downvote a comment, or post.
 */
@Component({
    name: 'vote-controller',
    components: {
        MaterialIcon
    }
})
export default class VoteController extends PostVoterMixin {
    /**
     * The post being voted on.
     */
    @Prop()
    public post!: Post;

    /**
     * Check to see if the resource was upvoted.
     */
    get isUpvoted() {
        if (this.post.vote == null) {
            return VoteDirection.Neutral;
        }

        return this.post.vote.direction === VoteDirection.Up;
    }

    /**
     * Check to see if the resource was downvoted.
     */
    get isDownvoted() {
        if (this.post.vote == null) {
            return VoteDirection.Neutral;
        }

        return this.post.vote.direction === VoteDirection.Down;
    }

    /**
     * Fancily formatted karma number.
     */
    get karma() {
        return NumberUtils.formatWithK(this.post.karma);
    }

    /**
     * Upvote the post.
     */
    public async upvote() {
        if (!this.$isLoggedIn()) {
            this.$redirectToLogin();
            return;
        }

        const direction =
            this.post.vote != null && this.post.vote.direction === VoteDirection.Up
                ? VoteDirection.Neutral
                : VoteDirection.Up;
        await this.$vote(this.post.id, direction);
    }

    /**
     * Downvote the post.
     */
    public async downvote() {
        if (!this.$isLoggedIn()) {
            this.$redirectToLogin();
            return;
        }

        const direction =
            this.post.vote != null && this.post.vote.direction === VoteDirection.Down
                ? VoteDirection.Neutral
                : VoteDirection.Down;
        await this.$vote(this.post.id, direction);
    }
}
</script>