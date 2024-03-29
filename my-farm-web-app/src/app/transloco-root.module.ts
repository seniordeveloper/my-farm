import { HttpClient } from "@angular/common/http";
import { Injectable, NgModule } from "@angular/core";
import {
	TRANSLOCO_LOADER,
	Translation,
	TranslocoLoader,
	TRANSLOCO_CONFIG,
	translocoConfig,
	TranslocoModule
} from "@ngneat/transloco";
import { Observable } from "rxjs";
import { environment } from "./environments/environment";

@Injectable({ providedIn: "root" })
export class TranslocoHttpLoader implements TranslocoLoader {
	constructor(private readonly http: HttpClient) { }

	getTranslation(lang: string): Observable<Translation> {
		return this.http.get<Translation>(`/assets/i18n/${lang}.json`);
	}
}

@NgModule({
	exports: [TranslocoModule],
	providers: [
		{
			provide: TRANSLOCO_CONFIG,
			useValue: translocoConfig({
				availableLangs: ["en"],
				defaultLang: "en",
				fallbackLang: "en",
				failedRetries: 4,
				prodMode: environment.production
			})
		},
		{ provide: TRANSLOCO_LOADER, useClass: TranslocoHttpLoader }
	]
})
export class TranslocoRootModule { }
