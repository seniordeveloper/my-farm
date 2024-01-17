import { Component, EventEmitter, Input, Output } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

@Component({
    selector: "app-confirmation-popup",
    templateUrl: "./confirmation-popup.component.html",
    styleUrls: ["./confirmation-popup.component.scss"]
})
export class ConfirmationPopupComponent {
    @Input() title: string = '';

    @Input() bodyText: string[] = [];

    @Input() acceptOnly = false;

    @Input() acceptButtonText = "Accept";

    @Input() cancelButtonText = "Cancel";

    @Input() acceptButtonClass = "btn-success";

    @Output() chose: EventEmitter<boolean> = new EventEmitter();

    constructor(public activeModal: NgbActiveModal) { }

    public get onAcceptEventEmitter(): EventEmitter<boolean> {
        return this.chose;
    }

    public accept(): void {
        this.chose.emit(true);
        this.activeModal.close(true);
    }

    public cancel(): void {
        this.chose.emit(false);
        this.activeModal.close(false);
    }
}
