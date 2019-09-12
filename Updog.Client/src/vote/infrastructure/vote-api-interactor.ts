import { ApiInteractor } from '@/core/api-interactor';
import { VoteMapper } from '@/vote/infrastructure/vote-mapper';

/**
 * Interactor to work with the vote portion of the API.
 */
export abstract class VoteApiInteractor<TInput, TOutput> extends ApiInteractor<TInput, TOutput> {
    /**
     * Mapper to rebuild vote entities.
     */
    protected voteMapper: VoteMapper;

    /**
     * Create a new vote api interactor
     */
    constructor(authToken: string = '') {
        super(authToken);
        this.voteMapper = new VoteMapper();
    }
}
