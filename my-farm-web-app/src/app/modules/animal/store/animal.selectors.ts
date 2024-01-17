import { createFeatureSelector, createSelector } from "@ngrx/store";
import { animalFeatureKey } from "./animal.reducers";
import { IAnimalState } from "./animal.state";

export const selectUserState =
    createFeatureSelector<IAnimalState>(animalFeatureKey);

export const selectIsAnimalStateLoading = createSelector(
    selectUserState,
    (state: IAnimalState) => state?.loadingRequestCount > 0 ?? false
);
