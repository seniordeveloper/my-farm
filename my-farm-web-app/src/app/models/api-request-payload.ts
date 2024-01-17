import { Observable } from "rxjs";

export class ApiRequestPayload<T> {
	constructor(
		public data: T,
		public cancellationObservable: Observable<void>
	) { }
}
