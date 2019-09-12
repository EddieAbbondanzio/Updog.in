import { Mapper } from '@/core/mapper';
import { Comment } from '@/comment/domain/comment';
import { UserMapper } from '@/user/infrastructure/user-mapper';

/**
 * Mapper to convert a comment into it's entity.
 */
export class CommentMapper implements Mapper<{ [key: string]: any }, Comment> {
    /**
     * Create a new comment mapper.
     * @param userMapper The user mapper.
     */
    constructor(private userMapper: UserMapper) {}

    /**
     * Map an object literal into a comment.
     * @param source The object literal to convert.
     */
    public map(source: { [key: string]: any }): Comment {
        if (typeof source.user !== 'object') {
            throw new TypeError('user must be of type object');
        }

        if (typeof source.id !== 'number') {
            throw new TypeError('id must be of type number');
        }

        if (typeof source.body !== 'string') {
            throw new TypeError('body must be of type string');
        }

        if (!Array.isArray(source.children)) {
            throw new TypeError('children must be an array');
        }

        const user = this.userMapper.map(source.user);
        const comment = new Comment(
            source.id,
            user,
            source.body,
            new Date(source.creationDate),
            source.wasUpdated,
            source.wasDeleted,
            source.upvotes,
            source.downvotes
        );

        comment.children = source.children.map(c => this.map(c));

        return comment;
    }
}
