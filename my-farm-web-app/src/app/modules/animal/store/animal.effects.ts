import { Injectable, inject } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { AnimalActions } from "./animal.actions";
import { AnimalApiService } from "../services/animal-api.service";
import { catchError, map, mergeMap } from "rxjs/operators";
import { of, pipe } from "rxjs";
import { ApiRequestPayload } from "src/app/models/api-request-payload";
import { SearchRequest } from "src/app/models/search-request";
import { AnimalModel } from "src/app/models/animal-model";


@Injectable()
export class AnimalEffects {
    private actions$ = inject(Actions);

    loadUsers$ = createEffect(() =>
        this.actions$.pipe(
            ofType(AnimalActions.LoadAnimals),
            mergeMap(
                (payload: ApiRequestPayload<SearchRequest>) =>
                    this.animalApi
                        .search(payload.data, payload.cancellationObservable)
                        .pipe(
                            map((x) => ({
                                type: AnimalActions.AnimalsLoaded,
                                data: x
                            })),
                            catchError(() =>
                                of({ type: AnimalActions.ErrorOccured }))
                        )
            )
        )
    );

    deleteAnimal$ = createEffect(() =>
        this.actions$.pipe(
            ofType(AnimalActions.DeleteAnimal),
            mergeMap((payload: ApiRequestPayload<number>) => {
                const request = this.animalApi.delete(
                    payload.data,
                    payload.cancellationObservable
                );

                return request.pipe(
                    map(() => ({ data: payload.data, type: AnimalActions.AnimalDeleted })),
                    catchError(() => of({ type: AnimalActions.ErrorOccured }))
                );
            })
        )
    );

    createAnimal$ = createEffect(() =>
        this.actions$.pipe(
            ofType(AnimalActions.CreateAnimal),
            mergeMap((payload: ApiRequestPayload<AnimalModel>) =>
                this.animalApi
                    .create(payload.data, payload.cancellationObservable)
                    .pipe(
                        map((x) => ({
                            type: AnimalActions.AnimalCreated,
                            data: x.data
                        })),
                        catchError(() => of({ type: AnimalActions.ErrorOccured }))
                    )
            )
        )
    );

    updateAnimal$ = createEffect(() =>
        this.actions$.pipe(
            ofType(AnimalActions.UpdateAnimal),
            mergeMap((payload: ApiRequestPayload<AnimalModel>) =>
                this.animalApi
                    .update(payload.data, payload.cancellationObservable)
                    .pipe(
                        map((x) => ({
                            type: AnimalActions.AnimalUpdated,
                            data: x.data
                        })),
                        catchError(() => of({ type: AnimalActions.ErrorOccured }))
                    )
            )
        )
    );

    constructor(
        private readonly animalApi: AnimalApiService
    ) { }
}
