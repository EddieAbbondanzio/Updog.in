import { ApiInteractor } from '@/core/interactors/api-interactor';
import { UserCredentials } from '../../domain/user-credentials';
import { UserLogin } from '../../domain/user-login';
import { User } from '../../domain/user';
import { UserMapper } from '@/user/infrastructure/user-mapper';
import { UserApiInteractor } from '@/user/infrastructure/user-api-interactor';

/**
 * Interactor to handle logging in a user.
 */
export class UserLoginInteractor extends UserApiInteractor<UserCredentials, UserLogin> {
    /**
     * Log into an existing user's account.
     * @param creds The credentials to authenticate.
     */
    public async handle(creds: UserCredentials): Promise<UserLogin> {
        const response = await this.http.post<UserLogin>('/session/', creds);

        return new UserLogin(this.userMapper.map(response.data.user), response.data.authToken);
    }
}
