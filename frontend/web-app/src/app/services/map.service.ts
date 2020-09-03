import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { MapHttpService } from './http/map.http.service';
import { ServiceResponse } from '../dtos/service-response.dto';
import { TileLayer } from 'leaflet';

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
    public async getTiles(): Promise<ServiceResponse<TileLayer, MapGetResponse>> {
        return new Promise<ServiceResponse<TileLayer, MapGetResponse>>(async (res) => {
            const response: ServiceResponse<TileLayer, MapGetResponse> = {
                responseCode: undefined,
                data: undefined
            };
            try {
                response.data = this.mapTiles();
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
