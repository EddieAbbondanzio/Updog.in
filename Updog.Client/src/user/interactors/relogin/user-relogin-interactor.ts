import { ApiInteractor } from '@/core/interactors/api-interactor';
import { UserCredentials } from '../../domain/user-credentials';
import { UserLogin } from '../../domain/user-login';
import { User } from '../../domain/user';
import { UserMapper } from '@/user/infrastructure/user-mapper';
import { UserApiInteractor } from '@/user/infrastructure/user-api-interactor';

/**
 * Interactor to handle re-logging in a user.
 */
export class UserReLoginInteractor extends UserApiInteractor<string, UserLogin> {
    /**
     * Relogin into an existing user's account.
     * @param authToken The token to authenticate.
     */
    public async handle(authToken: string): Promise<UserLogin> {
        const response = await this.http.patch<UserLogin>(
            '/session/',
            {},
            { headers: { Authorization: `Bearer ${authToken}` } }
        );

        return new UserLogin(this.userMapper.map(response.data.user), response.data.authToken);
    }
}
