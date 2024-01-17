import "@angular/localize/init";
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppBrowserModule } from "./app/app.browser.module";

export function getBaseUrl(): string {
  return document.getElementsByTagName("base")[0].href;
}

const providers = [{ provide: "BASE_URL", useFactory: getBaseUrl, deps: [] }];


document.addEventListener("DOMContentLoaded", () => {
  platformBrowserDynamic(providers)
    .bootstrapModule(AppBrowserModule)
    .catch((err) => console.error(err));
});

