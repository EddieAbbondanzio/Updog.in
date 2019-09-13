import { UserEntity } from '@/user/common/user-entity';
import { VoteResourceType } from '../domain/vote-resource-type';
import { VoteDirection } from '../domain/vote-direction';
import { Vote } from '../domain/vote';

/**
 * Base class for a resource that can be voted on.
 */
export abstract class VotableEntity extends UserEntity {
    /**
     * The current vote state of the user viewing it.
     */
    public abstract vote: Vote | null;

    /**
     * How many upvotes the resource has recieved.
     */
    public abstract upvotes: number;

    /**
     * How many downvotes the resource has recieved.
     */
    public abstract downvotes: number;

    /**
     * The type of vote resource it is.
     */
    public abstract voteResourceType: VoteResourceType;

    /**
     * The karma points the post has recieved (upvotes - downvotes).
     */
    get karma() {
        return this.upvotes - this.downvotes;
    }

    get upvoteRatio() {
        return (this.upvotes / (this.upvotes + this.downvotes)) * 100;
    }

    /**
     * Apply a vote to the entity.
     * @param direction The direction of the vote to apply.
     */
    public applyVote(direction: VoteDirection) {
        this.undoVote();

        switch (direction) {
            case VoteDirection.Up:
                this.vote = new Vote(VoteResourceType.Post, this.id, VoteDirection.Up);
                this.upvotes++;
                break;
            case VoteDirection.Neutral:
                this.vote = new Vote(VoteResourceType.Post, this.id, VoteDirection.Neutral);
                break;
            case VoteDirection.Down:
                this.vote = new Vote(VoteResourceType.Post, this.id, VoteDirection.Down);
                this.downvotes--;
                break;
        }
    }

    /**
     * Helper to undo a vote on the post.
     */
    private undoVote() {
        if (this.vote != null) {
            switch (this.vote.direction) {
                case VoteDirection.Up:
                    this.upvotes--;
                    break;
                case VoteDirection.Down:
                    this.downvotes--;
                    break;
            }

            this.vote = null;
        }
    }
}
