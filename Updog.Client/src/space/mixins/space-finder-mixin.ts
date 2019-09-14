import Mixin from 'vue-class-component';
import Vue from 'vue';
import { UserAuthMixin } from '@/user/mixins/user-auth-mixin';
import { getModule } from 'vuex-module-decorators';
import SpaceModule from '../store/space-store';

/**
 * Mixin to find spaces.
 */
@Mixin
export class SpaceFinderMixin extends UserAuthMixin {
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
