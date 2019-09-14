import { SpaceApiInteractor } from '@/space/infrastructure/space-api-interactor';
import { Space } from '@/space/domain/space';

/**
 * Interactor to find a space by name.
 */
export class SpaceFinderByName extends SpaceApiInteractor<string, Space> {
    public async handle(spaceName: string): Promise<Space> {
        const response = await this.http.get<Space>(`/space/${spaceName}`);
        return this.spaceMapper.map(response.data);
    }
}
