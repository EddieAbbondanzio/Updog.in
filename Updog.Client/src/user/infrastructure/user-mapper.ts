import { Mapper } from '@/core/mapper';
import { User } from '../domain/user';

/**
 * Mapper to convert a user from it's raw object to an
 * entity.
 */
export class UserMapper implements Mapper<{ [key: string]: any }, User> {
    /**
     * Map a raw object literal to a user.
     * @param source The source object.
     */
    public map(source: { [key: string]: any }): User {
        if (typeof source.id !== 'number') {
            throw new TypeError('id must of type number');
        }

        if (typeof source.username !== 'string') {
            throw new TypeError('username must be of type string');
        }

        if (typeof source.joinedDate !== 'string') {
            throw new TypeError('joinedDate must be a date string');
        }

        return new User(source.id, source.username, new Date(source.joinedDate), source.postKarma, source.commentKarma);
    }
}
