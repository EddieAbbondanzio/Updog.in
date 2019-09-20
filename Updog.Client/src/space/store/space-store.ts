import { VuexModule, Module, Action, Mutation } from 'vuex-module-decorators';
import { Space } from '../domain/space';
import { SpaceMutation } from './space-mutation';
import { SpaceFinderBySubscribed } from '../interactors/find-subscribed/space-finder-by-subscribed';
import { SpaceFinderByDefault } from '../interactors/find-default/space-finder-by-default';
import { SpaceFinderByName } from '../interactors/find-by-name/space-finder-by-name';
import { StoreNamespace } from '@/core/store/store-namespace';
import { SpaceAction } from './space-action';

/**
 * Module for space
 */
@Module({ namespaced: true, name: StoreNamespace.Space })
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
    public [SpaceMutation.ClearSubscribed]() {
        this.subscribed = [];
    }

    @Mutation
    public [SpaceMutation.SetDefault](spaces: Space[]) {
        this.default = spaces;
    }

    /**
     * Find the subscribed spaces of the user.
     */
    @Action({ rawError: true })
    public async [SpaceAction.FindSubscribedSpaces]() {
        const spaces = await new SpaceFinderBySubscribed(this.context.rootGetters['user/authToken']).handle();
        this.context.commit(SpaceMutation.SetSubscribed, spaces);

        return spaces;
    }

    /**
     * Find the default spaces.
     */
    @Action({ rawError: true })
    public async [SpaceAction.FindDefaultSpaces]() {
        const spaces = await new SpaceFinderByDefault(this.context.rootGetters['user/authToken']).handle();
        this.context.commit(SpaceMutation.SetDefault, spaces);

        return spaces;
    }

    /**
     * Find a space via it's unique name.
     * @param spaceName The name of the space to find.
     */
    @Action({ rawError: true })
    public async [SpaceAction.FindSpace](spaceName: string) {
        return new SpaceFinderByName(this.context.rootGetters['user/authToken']).handle(spaceName);
    }
}
