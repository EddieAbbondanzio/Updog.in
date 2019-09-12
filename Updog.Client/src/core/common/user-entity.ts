import { User } from '@/user/domain/user';

/**
 * An entity owned by a user.
 */
export abstract class UserEntity {
    /**
     * The user that owns the entity.
     */
    public abstract user: User;

    /**
     * Check to see if a user is the owner of the entity.
     * @param user The user to check for ownership.
     */
    public isOwner(user: User) {
        return this.user.id === user.id;
    }
}
