import { Subject } from "rxjs";
import { Injectable, OnDestroy } from "@angular/core";
import { ApiRequestPayload } from "../../models/api-request-payload";
import { BaseComponent } from "./base-component";

@Injectable()
export abstract class BaseApiComponent
    extends BaseComponent
    implements OnDestroy {
    protected cancellationSubject = new Subject<void>();
    protected cancellationObservable = this.cancellationSubject.asObservable();

    isLoading = false;

    protected constructor() {
        super();
    }

    ngOnDestroy(): void {
        this.cancellationSubject.next();
        this.cancellationSubject.complete();
    }

    protected createRequestPayload<T>(data?: T): ApiRequestPayload<T> {
        return new ApiRequestPayload(
            data && JSON.parse(JSON.stringify(data)),
            this.cancellationSubject.asObservable()
        );
    }
}
