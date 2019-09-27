import Mixin from 'vue-class-component';
import { getModule } from 'vuex-module-decorators';
import SpaceModule from '../store/space-store';
import AuthenticatedMixin from '@/user/mixins/authenticated-mixin';

/**
 * Mixin to find spaces.
 */
@Mixin
export default class SpaceFinderMixin extends AuthenticatedMixin {
    get $defaultSpaces() {
        const spaceModule = getModule(SpaceModule, this.$store);
        return spaceModule.default;
    }

    get $subscribedSpaces() {
        const spaceModule = getModule(SpaceModule, this.$store);
        return spaceModule.subscribed;
    }

    /**
     * Find the default spaces.
     */
    public async $findDefaultSpaces() {
        const spaceModule = getModule(SpaceModule, this.$store);
        return spaceModule.findDefaultSpaces();
    }

    /**
     * Find the subscribed spaces of the user.
     */
    public async $findSubscribedSpaces() {
        if (!this.$isLoggedIn()) {
            throw new Error('No user logged in');
        }

        const spaceModule = getModule(SpaceModule, this.$store);
        return spaceModule.findSubscribedSpaces();
    }
}
