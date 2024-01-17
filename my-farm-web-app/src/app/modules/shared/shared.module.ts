import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import {
    NgbDatepickerModule,
    NgbDropdownModule,
    NgbTypeaheadModule
} from "@ng-bootstrap/ng-bootstrap";
import { TranslocoModule } from "@ngneat/transloco";
import { RouterModule } from "@angular/router";
import { LoadingSpinnerComponent } from "./components/loading-spinner/loading-spinner.component";
import { NotFoundComponent } from "./components/not-found/not-found.component";
import { ConfirmationPopupComponent } from "./components/confirmation-popup/confirmation-popup.component";


@NgModule({
    declarations: [
        LoadingSpinnerComponent,
        NotFoundComponent,
        ConfirmationPopupComponent

    ],
    imports: [
        CommonModule,
        NgbDatepickerModule,
        NgbDropdownModule,
        FormsModule,
        TranslocoModule,
        RouterModule,
        NgbTypeaheadModule,
    ],
    exports: [
        LoadingSpinnerComponent,
    ]
})
export class SharedModule { }
