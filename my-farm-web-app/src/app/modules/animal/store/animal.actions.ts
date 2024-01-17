import { createAction, props } from "@ngrx/store";
import { ApiRequestPayload } from "../../../models/api-request-payload";
import { ApiResponsePayload } from "../../../models/api-response-payload";
import { SearchRequest } from "../../../models/search-request";
import { SearchResponse } from "src/app/models/search-response";
import { AnimalModel } from "src/app/models/animal-model";

export enum AnimalActions {
    LoadAnimals = "[Animal] Load Animals",
    AnimalsLoaded = "[Animal] Animals Loaded",

    CreateAnimal = "[Animal] Create Animal",
    AnimalCreated = "[Animal] Animal Created",

    DeleteAnimal = "[Animal] Delete Animal",
    AnimalDeleted = "[Animal] Animal Deleted",

    UpdateAnimal = "[Animal] Update Animal",
    AnimalUpdated = "[Animal] Animal Updated",

    ErrorOccured = "[Animal] Error Occured"
}

export const loadAnimals = createAction(
    AnimalActions.LoadAnimals,
    props<ApiRequestPayload<SearchRequest>>()
);

export const animalsLoaded = createAction(
    AnimalActions.AnimalsLoaded,
    props<SearchResponse<AnimalModel>>()
);

export const deleteAnimal = createAction(
    AnimalActions.DeleteAnimal,
    props<ApiRequestPayload<number>>()
);

export const animalDeleted = createAction(
    AnimalActions.AnimalDeleted,
    props<ApiResponsePayload<number>>()
);

export const createAnimal = createAction(
    AnimalActions.CreateAnimal,
    props<ApiRequestPayload<AnimalModel>>()
);

export const animalCreated = createAction(
    AnimalActions.AnimalCreated,
    props<ApiResponsePayload<AnimalModel>>()
);

export const updateAnimal = createAction(
    AnimalActions.UpdateAnimal,
    props<ApiRequestPayload<AnimalModel>>()
);

export const animalUpdated = createAction(
    AnimalActions.AnimalUpdated,
    props<ApiRequestPayload<AnimalModel>>()
);

export const errorOccured = createAction(AnimalActions.ErrorOccured);
