import { NgModule } from "@angular/core";
import { StoreModule } from "@ngrx/store";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { EffectsModule } from "@ngrx/effects";
import { TRANSLOCO_SCOPE, TranslocoModule } from "@ngneat/transloco";
import { NgbTypeaheadModule } from "@ng-bootstrap/ng-bootstrap";
import { TranslocoLoadingService } from "src/app/services/transloco-loading.service";
import { TranslationScopes } from "src/app/common/translation-scopes";
import { HttpApiService } from "../../services/http/http-api.service";
import {
    animalFeatureKey,
    animalReducer
} from "./store/animal.reducers";
import { AnimalEffects } from "./store/animal.effects";
import { AnimalApiService } from "./services/animal-api.service";
import { AnimalRouterComponent } from "./animal.router.component";
import { SharedModule } from "../shared/shared.module";
import { AnimalListComponent } from "./components/animal-list/animal-list.component";
import { AnimalRoutingModule } from "./animal.routing.module";
import { AnimalEditComponent } from "./components/animal-edit/animal-edit.component";

@NgModule({
    declarations: [
        AnimalRouterComponent,
        AnimalListComponent,
        AnimalEditComponent
    ],
    imports: [
        SharedModule,
        CommonModule,
        FormsModule,
        TranslocoModule,
        NgbTypeaheadModule,
        EffectsModule.forFeature([AnimalEffects]),
        StoreModule.forFeature(animalFeatureKey, animalReducer),
        AnimalRoutingModule
    ],
    providers: [
        AnimalApiService,
        HttpApiService,
        TranslocoLoadingService,
        { provide: TRANSLOCO_SCOPE, useValue: TranslationScopes.Animal }
    ]
})
export class AnimalModule { }
