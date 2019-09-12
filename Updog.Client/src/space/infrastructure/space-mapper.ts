import { Mapper } from '@/core/mapper';
import { UserMapper } from '@/user/common/user-mapper';
import { VoteMapper } from '@/vote/common/vote-mapper';
import { Space } from './space';

/**
 * Mapper to convert a space into it's entity form from a raw
 * object literal.
 */
export class SpaceMapper implements Mapper<{ [key: string]: any }, Space> {
    /**
     * Create a new space mapper.
     * @param userMapper The user mapper for converting user entities.
     */
    constructor(private userMapper: UserMapper) {}

    /**
     * Convert an object literal into it's post entity.
     * @param source The raw object literal.
     */
    public map(source: { [key: string]: any }): Space {
        if (typeof source.id !== 'number') {
            throw new TypeError('id must be an integer');
        }

        if (typeof source.name !== 'string') {
            throw new TypeError('title must be a string');
        }

        if (typeof source.description !== 'string') {
            throw new TypeError('body must be a string');
        }

        if (typeof source.user !== 'object') {
            throw new TypeError('user must be an object');
        }

        if (typeof source.creationDate !== 'string') {
            throw new TypeError('creationDate must be a date string');
        }

        if (typeof source.subscriptionCount !== 'number') {
            throw new TypeError('commentCount must be an integer');
        }
        const user = this.userMapper.map(source.user);

        return new Space(
            source.id,
            source.name,
            source.description,
            source.subscriptionCount,
            new Date(source.creationDate),
            user
        );
    }
}
