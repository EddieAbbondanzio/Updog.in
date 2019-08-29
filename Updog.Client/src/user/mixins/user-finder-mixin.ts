import Mixin from 'vue-class-component';
import UserModule from '../store/user-module';
import { getModule } from 'vuex-module-decorators';
import { UserCredentials } from '../common/user-credentials';
import { UserLogin } from '../common/user-login';
import Vue from 'vue';
import { User } from '../common/user';

/**
 * Mixin to find users in the backend.
 */
@Mixin
export class UserFinderMixin extends Vue {
    /**
     * The Vuex store module for user data.
     */
    private userModule: UserModule = getModule(UserModule, this.$store);

    /**
     * Find a user via their username.
     * @param username The username to look for.
     */
    public $findUserByUsername(username: string): Promise<User> {
        throw new Error();
        // return this.userModule.findByUsername(username);
    }
}
