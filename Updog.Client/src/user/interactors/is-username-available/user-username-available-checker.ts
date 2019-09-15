import { UserApiInteractor } from '@/user/infrastructure/user-api-interactor';

/**
 * Interactor to check if a username is available for use.
 */
export class UserUsernameAvailableChecker extends UserApiInteractor<string, boolean> {
    public async handle(username: string): Promise<boolean> {
        try {
            await this.http.head(`/user/${username}`);
            return false;
        } catch (error) {
            return error.response.status === 404;
        }
    }
}
