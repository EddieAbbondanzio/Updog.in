import Mixin from 'vue-class-component';
import UserModule from '../store/user-store';
import { getModule } from 'vuex-module-decorators';
import Vue from 'vue';

/**
 * Mixin to find users in the backend.
 */
@Mixin
export default class UserFinderMixin extends Vue {
    /**
     * Find a user via their username.
     * @param username The username to look for.
     */
    public async $findUserByUsername(username: string) {
        const userModule: UserModule = getModule(UserModule, this.$store);
        return userModule.findByUsername(username);
    }
}
