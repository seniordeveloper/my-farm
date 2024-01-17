import { HttpClient, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { takeUntil } from "rxjs/operators";
import { combineCompleteUrl } from "src/app/extensions/utils";

@Injectable()
export class HttpApiService {
	constructor(private readonly http: HttpClient) { }

	public get<T>(
		url: string,
		data?: any,
		cancellationSubject?: Observable<void>
	): Observable<T> {
		let request = this.http.get<T>(combineCompleteUrl(url, data));

		if (cancellationSubject) {
			request = this.applyCancellationSubject(
				request,
				cancellationSubject
			);
		}

		return request;
	}

	public post<T>(
		url: string,
		// eslint-disable-next-line @typescript-eslint/ban-types
		data?: object,
		cancellationSubject?: Observable<void>
	): Observable<T> {
		let request = this.http.post<T>(combineCompleteUrl(url), data);

		if (cancellationSubject) {
			request = this.applyCancellationSubject(
				request,
				cancellationSubject
			);
		}

		return request;
	}

	public put<T>(
		url: string,
		// eslint-disable-next-line @typescript-eslint/ban-types
		data: object,
		cancellationSubject?: Observable<void>
	): Observable<T> {
		let request = this.http.put<T>(combineCompleteUrl(url), data);

		if (cancellationSubject) {
			request = this.applyCancellationSubject(
				request,
				cancellationSubject
			);
		}

		return request;
	}

	public delete<T>(
		url: string,
		data: any,
		cancellationSubject?: Observable<void>
	): Observable<T> {
		let request = this.http.delete<T>(combineCompleteUrl(url, data));

		if (cancellationSubject) {
			request = this.applyCancellationSubject(
				request,
				cancellationSubject
			);
		}

		return request;
	}

	private applyCancellationSubject<T>(
		request: Observable<T>,
		cancellationSubject: Observable<void>
	): Observable<T> {
		return request.pipe(takeUntil(cancellationSubject));
	}
}
