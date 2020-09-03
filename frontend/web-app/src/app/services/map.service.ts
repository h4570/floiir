import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { MapHttpService } from './http/map.http.service';
import { ServiceResponse } from '../models/service-response.model';

export enum MapGetResponse {
    Success,
    UnknownHttpError,
    UnknownError
}

@Injectable()
export class MapService extends MapHttpService {



    public error: any;

    private keyCheckResponse: MapGetResponse;

    /** Get tiles to build map from api base on x,y,z. */
    public async getTiles(): Promise<ServiceResponse> {
        return new Promise<ServiceResponse>(async (res) => {
            const response: ServiceResponse = {
                responseCode: undefined,
                data: undefined
            };
            try {
                response.data = await this.mapTiles();
                response.responseCode = MapGetResponse.Success;
            } catch (err) {
                this.error = err;
                if (err instanceof HttpErrorResponse) {
                    response.responseCode = MapGetResponse.UnknownHttpError;
                } else response.responseCode = MapGetResponse.UnknownError;
            }
            res(response);
        });
    }
}
