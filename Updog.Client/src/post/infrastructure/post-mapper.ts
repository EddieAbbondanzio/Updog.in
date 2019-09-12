import { Mapper } from '@/core/mapper';
import { Post } from '../domain/post';
import { UserMapper } from '@/user/infrastructure/user-mapper';
import { VoteMapper } from '@/vote/infrastructure/vote-mapper';
import { SpaceMapper } from '@/space/infrastructure/space-mapper';

/**
 * Mapper to convert a post into it's entity form from a raw
 * object literal.
 */
export class PostMapper implements Mapper<{ [key: string]: any }, Post> {
    /**
     * Create a new post mapper.
     * @param userMapper The user mapper for converting user entities.
     * @param spaceMapper Mapper to convert spaces.
     * @param voteMapper Mapper to convert votes.
     */
    constructor(private userMapper: UserMapper, private spaceMapper: SpaceMapper, private voteMapper: VoteMapper) {}

    /**
     * Convert an object literal into it's post entity.
     * @param source The raw object literal.
     */
    public map(source: { [key: string]: any }): Post {
        if (typeof source.id !== 'number') {
            throw new TypeError('id must be an integer');
        }

        if (typeof source.type !== 'number') {
            throw new TypeError('type must be an integer');
        }

        if (typeof source.title !== 'string') {
            throw new TypeError('title must be a string');
        }

        if (typeof source.body !== 'string') {
            throw new TypeError('body must be a string');
        }

        if (typeof source.user !== 'object') {
            throw new TypeError('user must be an object');
        }

        if (typeof source.space !== 'object') {
            throw new TypeError('space must be an object');
        }

        if (typeof source.creationDate !== 'string') {
            throw new TypeError('creationDate must be a date string');
        }

        if (typeof source.commentCount !== 'number') {
            throw new TypeError('commentCount must be an integer');
        }

        if (typeof source.upvotes !== 'number') {
            throw new TypeError('upvotes must be an integer');
        }

        if (typeof source.downvotes !== 'number') {
            throw new TypeError('downvotes must be an integer');
        }

        const user = this.userMapper.map(source.user);
        const space = this.spaceMapper.map(source.space);

        const p = new Post(
            source.id,
            source.type,
            source.title,
            source.body,
            user,
            space,
            new Date(source.creationDate),
            source.commentCount,
            source.wasUpdated,
            source.wasDeleted,
            source.upvotes,
            source.downvotes,
            source.vote
        );

        if (source.vote != null) {
            p.vote = this.voteMapper.map(source.vote);
        }

        return p;
    }
}
