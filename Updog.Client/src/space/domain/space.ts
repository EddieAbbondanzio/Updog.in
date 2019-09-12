import { UserEntity } from '@/core/common/user-entity';
import { User } from '@/user/common/user';

/**
 * Space (sub forum).
 */
export class Space extends UserEntity {
    /**
     * Create a new (safe) space.
     * @param id The unique ID of the space.
     * @param name The fancy name of the space.
     * @param desription The description of the space.
     * @param subscriptionCount The number of subscribers.
     * @param creationDate The creation date of the space.
     * @param user The user that created it.
     */
    public constructor(
        public id: number,
        public name: string,
        public desription: string,
        public subscriptionCount: number,
        public creationDate: Date,
        public user: User
    ) {
        super();
    }
}
