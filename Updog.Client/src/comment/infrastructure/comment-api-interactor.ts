import { CommentMapper } from './comment-mapper';
import { ApiInteractor } from '@/core/interactors/api-interactor';
import { UserMapper } from '@/user/infrastructure/user-mapper';
import { VoteMapper } from '@/vote/infrastructure/vote-mapper';

/**
 * Interactor for working with the comment portion of the API.
 */
export abstract class CommentApiInteractor<TInput, TOutput> extends ApiInteractor<TInput, TOutput> {
    /**
     * Mapper to rebuild comment entities.
     */
    protected commentMapper: CommentMapper;

    /**
     * Create a new comment api interactor.
     */
    constructor(authToken: string = '') {
        super(authToken);
        this.commentMapper = new CommentMapper(new UserMapper(), new VoteMapper());
    }
}
