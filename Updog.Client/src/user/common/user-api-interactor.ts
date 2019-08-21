import { ApiInteractor } from '@/core/api-interactor';
import { UserMapper } from './user-mapper';

/**
 * Interactor for the user API portion.
 */
export abstract class UserApiInteractor<TInput, TOutput> extends ApiInteractor<TInput, TOutput> {
    /**
     * Mapper to rebuild a user object.
     */
    protected userMapper: UserMapper;

    /**
     * Create a new user api interactor.
     */
    constructor() {
        super();
        this.userMapper = new UserMapper();
    }
}
