import { Action, createReducer, on } from "@ngrx/store";
import {
    animalsLoaded,
    loadAnimals,
    deleteAnimal,
    animalDeleted,
    createAnimal,
    animalCreated,
    updateAnimal,
    animalUpdated
} from "./animal.actions";
import { initialAnimalState, IAnimalState } from "./animal.state";

const _animalReducer = createReducer(
    initialAnimalState,
    on(loadAnimals, (state) => ({
        ...state,
        loadingRequestCount: state.loadingRequestCount + 1
    })),
    on(animalsLoaded, (state) => ({
        ...state,
        loadingRequestCount: state.loadingRequestCount - 1
    })),
    on(deleteAnimal, (state) => ({
        ...state,
        loadingRequestCount: state.loadingRequestCount + 1
    })),
    on(animalDeleted, (state) => ({
        ...state,
        loadingRequestCount: state.loadingRequestCount - 1
    })),
    on(createAnimal, (state) => ({
        ...state,
        loadingRequestCount: state.loadingRequestCount + 1
    })),
    on(animalCreated, (state) => ({
        ...state,
        loadingRequestCount: state.loadingRequestCount - 1
    })),
    on(updateAnimal, (state) => ({
        ...state,
        loadingRequestCount: state.loadingRequestCount + 1
    })),
    on(animalUpdated, (state) => ({
        ...state,
        loadingRequestCount: state.loadingRequestCount - 1
    })),
);

export function animalReducer(state: IAnimalState, action: Action): IAnimalState {
    return _animalReducer(state, action);
}

export const animalFeatureKey = "animal";
