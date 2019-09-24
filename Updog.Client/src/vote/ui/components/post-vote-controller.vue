<template>
    <div class="d-flex flex-column justify-center">
        <v-btn @click.native.stop="upvote" text icon>
            <v-icon size="32" :class="{ upvoted: isUpvoted}">keyboard_arrow_up</v-icon>
        </v-btn>
        <div class="d-flex flex-row justify-center text-center font-weight-bold text-muted text-md">
            <span
                :class="{ upvoted: isUpvoted, downvoted: isDownvoted}"
                :title="`${post.karma} points`"
            >{{ karma }}</span>
        </div>
        <v-btn @click.native.stop="downvote" text icon>
            <v-icon size="32" :class="{ downvoted: isDownvoted}">keyboard_arrow_down</v-icon>
        </v-btn>
    </div>
</template>

<style scoped>
.vote-arrow {
    height: 32px;
}

.vote-arrow i {
    font-size: 32px;
}
</style>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { PostVoterMixin } from '@/vote';
import { NumberUtils } from '@/core';
import { VoteDirection } from '@/vote';
import { Post } from '@/post';

/**
 * Control to upvote, or downvote a comment, or post.
 */
@Component({
    name: 'vote-controller'
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