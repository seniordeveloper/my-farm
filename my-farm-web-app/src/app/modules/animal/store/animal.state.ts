import { AnimalModel } from "src/app/models/animal-model";

export interface IAnimalState {
    loadingRequestCount: number;
}

export const initialAnimalState: IAnimalState = {
    loadingRequestCount: 0,
};
