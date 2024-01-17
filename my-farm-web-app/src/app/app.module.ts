import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule, NgbToastModule } from "@ng-bootstrap/ng-bootstrap";
import { StoreModule } from "@ngrx/store";
import { EffectsModule } from "@ngrx/effects";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule } from "@angular/forms";

import { AppRoutingModule } from './app-routing.module';
import { TranslocoRootModule } from "./transloco-root.module";
import { AppComponent } from './app.component';
import { environment } from "./environments/environment";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    TranslocoRootModule,
    HttpClientModule,
    NgbModule,
    NgbToastModule,
    StoreModule.forRoot(
      {},
      {
        runtimeChecks: {
          strictStateImmutability: false,
          strictActionImmutability: false
        }
      }
    ),
    EffectsModule.forRoot([]),
    ...environment.devModules,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
