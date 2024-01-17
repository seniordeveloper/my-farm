import { Component, OnInit } from "@angular/core";
import { UntilDestroy, untilDestroyed } from "@ngneat/until-destroy";
import { Store } from "@ngrx/store";
import { IAnimalState } from "../../store/animal.state";
import { BaseApiComponent } from "src/app/common/base/base-api-component";
import { AnimalActions, createAnimal, deleteAnimal, loadAnimals, updateAnimal } from "../../store/animal.actions";
import { SearchRequest } from "src/app/models/search-request";
import { Actions, ofType } from "@ngrx/effects";
import { SearchResponse } from "src/app/models/search-response";
import { AnimalModel } from "src/app/models/animal-model";
import { ApiResponsePayload } from "src/app/models/api-response-payload";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { ConfirmationPopupComponent } from "../../../shared/components/confirmation-popup/confirmation-popup.component";
import { translate } from "@ngneat/transloco";
import { AnimalEditComponent } from "../animal-edit/animal-edit.component";

@UntilDestroy()
@Component({
    selector: "app-animal-list",
    templateUrl: "./animal-list.component.html",
    styleUrls: ["./animal-list.component.scss"]
})
export class AnimalListComponent extends BaseApiComponent implements OnInit {

    animals: AnimalModel[] = [];
    totalCount: number = 0;

    constructor(
        private readonly store: Store<IAnimalState>,
        private readonly actions: Actions,
        private readonly modalService: NgbModal
    ) {
        super();

        this.actions
            .pipe(
                ofType(AnimalActions.AnimalsLoaded),
                untilDestroyed(this)
            )
            .subscribe(
                (action: {
                    data: ApiResponsePayload<SearchResponse<AnimalModel>>
                }) => {
                    this.animals = action.data.data.records;
                    this.totalCount = action.data.data.totalCount;
                }
            );

        this.actions
            .pipe(
                ofType(AnimalActions.AnimalDeleted),
                untilDestroyed(this)
            )
            .subscribe(
                (action: {
                    data: number
                }) => {
                    if (this.animals?.length) {
                        this.animals = this.animals.filter(x => x.id !== action.data);
                    }
                }
            );

        this.actions
            .pipe(
                ofType(AnimalActions.AnimalCreated),
                untilDestroyed(this)
            )
            .subscribe(
                (action: {
                    data: AnimalModel
                }) => {
                    console.log(action.data);
                }
            );
    }

    ngOnInit(): void {
        this.store.dispatch(loadAnimals(this.createRequestPayload({
            searchTerm: ''
        } as SearchRequest)));
    }

    async openDeletePopup(animal: AnimalModel): Promise<void> {
        const modalRef = this.modalService.open(ConfirmationPopupComponent);

        modalRef.componentInstance.title = translate(
            "animal.animals.deleteAnimalConfirmation.title"
        );
        modalRef.componentInstance.bodyText = [
            translate("animal.animals.deleteAnimalConfirmation.body")
        ];
        modalRef.componentInstance.acceptButtonClass = 'btn-danger';
        modalRef.componentInstance.acceptButtonText = 'Yes, Delele ((';

        if (await modalRef.result) {
            this.deleteAnimal(animal);
        }
    }

    async onAddNewOrEditAnimal(data: AnimalModel = null): Promise<void> {
        const modalRef = this.modalService.open(AnimalEditComponent);

        modalRef.componentInstance.title = translate(
            "animal.animals.addEditAnimal.addTitle"
        );

        modalRef.componentInstance.acceptButtonText = 'Submit';

        modalRef.componentInstance.model = data ?? { id: 0, name: null } as AnimalModel;

        const model = await modalRef.result as AnimalModel;
        if (model) {
            if (model.id) {
                this.store.dispatch(updateAnimal(this.createRequestPayload(model)))
            } else {
                this.store.dispatch(createAnimal(this.createRequestPayload(model)))
            }
        }
    }

    private deleteAnimal(animal: AnimalModel): void {
        this.store.dispatch(deleteAnimal(this.createRequestPayload(animal.id)));
    }
}
