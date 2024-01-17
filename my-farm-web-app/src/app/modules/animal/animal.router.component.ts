import { AfterViewInit, Component } from "@angular/core";
import { UntilDestroy, untilDestroyed } from "@ngneat/until-destroy";
import { Store } from "@ngrx/store";
import { IAnimalState } from "./store/animal.state";
import { selectIsAnimalStateLoading } from "./store/animal.selectors";
import { delay } from "rxjs/operators";
import { TranslocoLoadingService } from "src/app/services/transloco-loading.service";
import { TranslationScopes } from "src/app/common/translation-scopes";

@UntilDestroy()
@Component({
    selector: "app-animal-management",
    templateUrl: "./animal.router.component.html"
})
export class AnimalRouterComponent implements AfterViewInit {

    isLoading = false;
    isTranslationsLoading = true;

    constructor(
        private readonly store: Store<IAnimalState>,
        private readonly translocoLoadingService: TranslocoLoadingService
    ) { }

    ngAfterViewInit(): void {
        this.store
            .select(selectIsAnimalStateLoading)
            .pipe(untilDestroyed(this), delay(0))
            .subscribe((isLoading) => {
                this.isLoading = isLoading;
            });

        this.translocoLoadingService
            .scopeLoaded(TranslationScopes.Animal)
            .pipe(untilDestroyed(this), delay(0))
            .subscribe(() => {
                this.isTranslationsLoading = false;
            });
    }
}
