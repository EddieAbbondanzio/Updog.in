import { VuexModule, Module, Action, Mutation } from 'vuex-module-decorators';
import { Space } from '../domain/space';
import { SpaceMutation } from './space-mutation';
import { SpaceFinderBySubscribed } from '../interactors/find-subscribed/space-finder-by-subscribed';
import { SpaceFinderByDefault } from '../interactors/find-default/space-finder-by-default';
import { SpaceFinderByName } from '../interactors/find-by-name/space-finder-by-name';
import { StoreName } from '@/core/store/store-name';

/**
 * Module for space
 */
@Module({ namespaced: true, name: StoreName.Space })
export default class SpaceStore extends VuexModule {
    /**
     * Cache of the subscribed spaces.
     */
    public subscribed: Space[] = [];

    /**
     * Cache of the default spaces.
     */
    public default: Space[] = [];

    @Mutation
    public [SpaceMutation.SetSubscribed](spaces: Space[]) {
        this.subscribed = spaces;
    }

    @Mutation
    public [SpaceMutation.SetDefault](spaces: Space[]) {
        this.default = spaces;
    }

    /**
     * Find the subscribed spaces of the user.
     */
    @Action({ rawError: true })
    public async findSubscribedSpaces() {
        const spaces = await new SpaceFinderBySubscribed(this.context.rootGetters['user/authToken']).handle();
        this.context.commit(SpaceMutation.SetSubscribed, spaces);

        return spaces;
    }

    /**
     * Find the default spaces.
     */
    @Action({ rawError: true })
    public async findDefaultSpaces() {
        const spaces = await new SpaceFinderByDefault(this.context.rootGetters['user/authToken']).handle();
        this.context.commit(SpaceMutation.SetDefault, spaces);

        return spaces;
    }

    /**
     * Find a space via it's unique name.
     * @param spaceName The name of the space to find.
     */
    @Action({ rawError: true })
    public async findSpace(spaceName: string) {
        return new SpaceFinderByName(this.context.rootGetters['user/authToken']).handle(spaceName);
    }
}
