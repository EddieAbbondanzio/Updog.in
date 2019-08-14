import { ApiInteractor } from '@/core/api-interactor';
import { UserCredentials } from '../../common/user-credentials';
import { UserLogin } from '../../common/user-login';
import { User } from '../../common/user';

/**
 * Interactor to handle logging in a user.
 */
export class UserLoginInteractor extends ApiInteractor<UserCredentials, UserLogin> {
    /**
     * Log into an existing user's account.
     * @param creds The credentials to authenticate.
     */
    public async handle(creds: UserCredentials): Promise<UserLogin> {
        const response = await this.http.post<UserLogin>('/session/', creds);

        return new UserLogin(
            new User(
                response.data.user.id,
                response.data.user.username,
                response.data.user.joinedDate,
                response.data.user.email
            ),
            response.data.authToken
        );
    }
}
