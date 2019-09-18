import Mixin from 'vue-class-component';
import Vue from 'vue';
import { UserCredentials, UserLogin, UserStore } from '..';
import { getModule } from 'vuex-module-decorators';
import { AuthenticatedMixin } from './authenticated-mixin';

/**
 * Mixin to handle logging in and out a user.
 */
@Mixin
export class UserLoginMixin extends AuthenticatedMixin {
    /**
     * Log in the user with the backend.
     * @param userCreds The username / password combo to log in with.
     */
    public async $loginUser(userCreds: UserCredentials): Promise<UserLogin> {
        const userStore: UserStore = getModule(UserStore, this.$store);
        return userStore.login(userCreds);
    }

    /**
     * Re log in a user with an older token.
     * @param authToken The auth token to authenticate.
     */
    public async $reloginUser(authToken: string): Promise<UserLogin> {
        const userStore: UserStore = getModule(UserStore, this.$store);
        return userStore.relogin(authToken);
    }

    /**
     * Log out the user.
     */
    public async $logoutUser(): Promise<void> {
        const userStore: UserStore = getModule(UserStore, this.$store);
        return userStore.logout();
    }
}
