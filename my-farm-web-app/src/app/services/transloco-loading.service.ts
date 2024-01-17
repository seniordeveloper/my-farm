import { Injectable } from "@angular/core";
import { TranslocoService } from "@ngneat/transloco";
import { Observable, of } from "rxjs";
import { filter, map, tap } from "rxjs/operators";

@Injectable({ providedIn: "root" })
export class TranslocoLoadingService {
    private loadedModules: string[] = [];

    constructor(private readonly translateService: TranslocoService) { }

    scopeLoaded(module: string): Observable<any> {
        if (this.loadedModules.indexOf(module) >= 0) {
            return of(null);
        }

        return this.translateService.events$.pipe(
            tap((x) => {
                if (x.payload.scope) {
                    this.loadedModules.push(x.payload.scope);
                }
            }),
            filter((x) => x.payload.scope === module),
            map(() => { })
        );
    }
}
