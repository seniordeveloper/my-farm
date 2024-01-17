import { Component } from "@angular/core";
import { UntilDestroy } from "@ngneat/until-destroy";

@UntilDestroy()
@Component({
    selector: "app-not-found",
    templateUrl: "./not-found.component.html",
    styleUrls: ["./not-found.component.scss"]
})
export class NotFoundComponent {
}
