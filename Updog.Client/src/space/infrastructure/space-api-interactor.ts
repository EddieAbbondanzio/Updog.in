import { ApiInteractor } from '@/core/api-interactor';
import { UserMapper } from '@/user/infrastructure/user-mapper';
import { VoteMapper } from '@/vote/infrastructure/vote-mapper';
import { SpaceMapper } from './space-mapper';

/**
 * Interactor to work with the post portion of the API.
 */
export abstract class SpaceApiInteractor<TInput, TOutput> extends ApiInteractor<TInput, TOutput> {
    /**
     * Mapper to rebuild post entities.
     */
    protected spaceMapper: SpaceMapper;

    /**
     * Create a new post api interactor
     */
    constructor(authToken: string = '') {
        super(authToken);
        this.spaceMapper = new SpaceMapper(new UserMapper());
    }
}
