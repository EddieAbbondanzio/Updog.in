import Mixin from 'vue-class-component';
import UserModule from '../store/user-store';
import { getModule } from 'vuex-module-decorators';
import { UserCredentials } from '../domain/user-credentials';
import { UserLogin } from '../domain/user-login';
import Vue from 'vue';
import { User } from '../domain/user';

/**
 * Mixin to find users in the backend.
 */
@Mixin
export class UserFinderMixin extends Vue {
    /**
     * Find a user via their username.
     * @param username The username to look for.
     */
    public async $findUserByUsername(username: string) {
        const userModule: UserModule = getModule(UserModule, this.$store);
        await userModule.findByUsername(username);
        return userModule.users.find(u => u.username === username);
    }
}
