import { Injectable } from "@angular/core";
import { HttpApiService } from "../../../services/http/http-api.service";
import { SearchRequest } from "src/app/models/search-request";
import { Observable } from "rxjs";
import { SearchResponse } from "src/app/models/search-response";
import { AnimalModel } from "src/app/models/animal-model";
import { AnimalRoutes } from "src/app/common/api-routes";
import { ApiResponsePayload } from "src/app/models/api-response-payload";

@Injectable()
export class AnimalApiService {
    constructor(public httpService: HttpApiService) { }

    public search(
        request: SearchRequest,
        cancellationSubject: Observable<void>
    ): Observable<SearchResponse<AnimalModel>> {
        return this.httpService.get<SearchResponse<AnimalModel>>(
            `${AnimalRoutes.Root}/${AnimalRoutes.Search}`,
            request,
            cancellationSubject
        );
    }

    public delete(
        id: number,
        cancellationSubject: Observable<void>
    ): Observable<void> {
        return this.httpService.delete(
            `${AnimalRoutes.Root}/${AnimalRoutes.Delete}`,
            id,
            cancellationSubject
        );
    }

    public update(
        model: AnimalModel,
        cancellationSubject: Observable<void>
    ): Observable<ApiResponsePayload<AnimalModel>> {
        return this.httpService.put(
            `${AnimalRoutes.Root}/${AnimalRoutes.Update}`,
            model,
            cancellationSubject
        );
    }

    public create(
        model: AnimalModel,
        cancellationSubject: Observable<void>
    ): Observable<ApiResponsePayload<AnimalModel>> {
        return this.httpService.post(
            `${AnimalRoutes.Root}/${AnimalRoutes.Create}`,
            model,
            cancellationSubject
        );
    }
}
