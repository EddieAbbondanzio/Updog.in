import { ApiInteractor } from '@/core/api-interactor';
import { PostMapper } from './post-mapper';
import { UserMapper } from '@/user/infrastructure/user-mapper';
import { VoteMapper } from '@/vote/infrastructure/vote-mapper';
import { SpaceMapper } from '@/space/infrastructure/space-mapper';

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
