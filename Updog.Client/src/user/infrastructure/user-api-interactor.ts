import { ApiInteractor } from '@/core/interactors/api-interactor';
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
    constructor(authToken: string = '') {
        super(authToken);
        this.userMapper = new UserMapper();
    }
}
