import { BaseFormComponent } from "src/app/common/base/base-form-component";
import { AnimalModel } from "src/app/models/animal-model";
import { UntilDestroy } from "@ngneat/until-destroy";
import { Component, ViewChild, Input, Output, EventEmitter } from "@angular/core";
import { NgForm } from "@angular/forms";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

@UntilDestroy()
@Component({
    selector: "app-edit-animal",
    templateUrl: "./animal-edit.component.html",
    styleUrls: ["./animal-edit.component.scss"]
})
export class AnimalEditComponent extends BaseFormComponent<AnimalModel> {
    @ViewChild("form") override form: NgForm;

    @Input() title: string = '';

    @Input() bodyText: string[] = [];

    @Input() acceptOnly = false;

    @Input() acceptButtonText = "Accept";

    @Input() cancelButtonText = "Cancel";

    @Input() acceptButtonClass = "btn-success";

    @Output() chose: EventEmitter<AnimalModel> = new EventEmitter();

    constructor(public activeModal: NgbActiveModal) {
        super();
    }

    public get onAcceptEventEmitter(): EventEmitter<AnimalModel> {
        return this.chose;
    }

    public submit(): void {
        this.chose.emit(this.model);
        this.activeModal.close(this.model);
    }

    public cancel(): void {
        this.chose.emit(null);
        this.activeModal.close(null);
    }
}
