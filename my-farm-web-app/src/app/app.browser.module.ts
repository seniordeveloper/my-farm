import { NgModule } from "@angular/core";
import {
	BrowserModule,
} from "@angular/platform-browser";

import { AppModule } from "./app.module";
import { AppComponent } from "./app.component";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

@NgModule({
	imports: [
		AppModule,
		BrowserModule,
		NgbModule
	],
	bootstrap: [AppComponent]
})
export class AppBrowserModule { }
