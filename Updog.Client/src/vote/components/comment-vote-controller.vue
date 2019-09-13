<template>
    <div class="d-flex flex-column justify-content-top">
        <b-button variant="link" @click="upvote()" class="p-0 m-0 vote-arrow">
            <material-icon
                icon="keyboard_arrow_up"
                variant="muted"
                :class="{ upvoted: isUpvoted}"
                color="red"
            />
        </b-button>
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
.vote-arrow {
    height: 24px;
}
</style>

<script lang="ts">
import { Component, Vue, Prop } from 'vue-property-decorator';
import { Comment } from '../../comment/domain/comment';
import MaterialIcon from '@/core/components/material-icon.vue';
import { VoteDirection } from '../domain/vote-direction';
import { NumberUtils } from '@/core/utils/number-utils';
import { CommentVoterMixin } from '@/vote/mixins/comment-voter-mixin';

/**
 * Controller to handle the votes of a comment.
 */
@Component({
    name: 'comment-vote-controller',
    components: {
        MaterialIcon
    }
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