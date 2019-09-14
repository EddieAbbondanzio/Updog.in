import { ApiInteractor } from '@/core/interactors/api-interactor';
import { PostMapper } from './post-mapper';
import { UserMapper } from '@/user';
import { VoteMapper } from '@/vote';
import { SpaceMapper } from '@/space';

/**
 * Interactor to work with the post portion of the API.
 */
export abstract class PostApiInteractor<TInput, TOutput> extends ApiInteractor<TInput, TOutput> {
    /**
     * Mapper to rebuild post entities.
     */
    protected postMapper: PostMapper;

    /**
     * Create a new post api interactor
     */
    constructor(authToken: string = '') {
        super(authToken);
        this.postMapper = new PostMapper(new UserMapper(), new SpaceMapper(new UserMapper()), new VoteMapper());
    }
}
