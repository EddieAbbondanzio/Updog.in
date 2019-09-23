<template>
    <div class="d-flex flex-column justify-content-top">
        <v-btn icon @click="upvote()" class="vote-arrow">
            <v-icon :class="{ upvoted: isUpvoted}">keyboard_arrow_up</v-icon>
        </v-btn>
        <v-btn icon @click="downvote()" class="vote-arrow">
            <v-icon :class="{downvoted: isDownvoted}">keyboard_arrow_down</v-icon>
        </v-btn>
    </div>
</template>

<style scoped>
.v-btn:hover:before,
.v-btn:focus:before {
    color: transparent !important;
}

.vote-arrow {
    height: 24px;
}
</style>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Comment } from '@/comment';
import { NumberUtils } from '@/core';
import { VoteDirection, CommentVoterMixin } from '@/vote';

/**
 * Controller to handle the votes of a comment.
 */
@Component({
    name: 'comment-vote-controller'
})
export default class CommentVoteController extends CommentVoterMixin {
    /**
     * The comment being voted on.
     */
    @Prop()
    public comment!: Comment;

    /**
     * Check to see if the resource was upvoted.
     */
    get isUpvoted() {
        if (this.comment.vote == null) {
            return VoteDirection.Neutral;
        }

        return this.comment.vote.direction === VoteDirection.Up;
    }

    /**
     * Check to see if the resource was downvoted.
     */
    get isDownvoted() {
        if (this.comment.vote == null) {
            return VoteDirection.Neutral;
        }

        return this.comment.vote.direction === VoteDirection.Down;
    }

    /**
     * Fancily formatted karma number.
     */
    get karma() {
        return NumberUtils.formatWithK(this.comment.karma);
    }

    public async upvote() {
        if (!this.$isLoggedIn()) {
            this.$redirectToLogin();
            return;
        }

        const direction =
            this.comment.vote != null && this.comment.vote.direction === VoteDirection.Up
                ? VoteDirection.Neutral
                : VoteDirection.Up;
        await this.$vote(this.comment.id, direction);
    }

    public async downvote() {
        if (!this.$isLoggedIn()) {
            this.$redirectToLogin();
            return;
        }

        const direction =
            this.comment.vote != null && this.comment.vote.direction === VoteDirection.Down
                ? VoteDirection.Neutral
                : VoteDirection.Down;
        await this.$vote(this.comment.id, direction);
    }
}
</script>