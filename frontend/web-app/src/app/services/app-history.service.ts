import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { AppHistoryAdapter } from '../adapters/app-history.adapter';
import { AppHistory, AppTable } from '../models/app-history.model';

@Injectable()
export class AppHistoryService {

    private readonly historyAdapter: AppHistoryAdapter;

    constructor(
        private readonly http: HttpClient
    ) {
        this.historyAdapter = new AppHistoryAdapter();
    }

    public async get(tableId: AppTable, elementId: number = null): Promise<AppHistory[]> {
        const uri = elementId
            ? `app-history/table/${tableId}/element/${elementId}`
            : `app-history/table/${tableId}`;
        return this.http
            .get<any[]>(`${environment.urls.api}` + uri)
            .toPromise()
            .then(raws => this.historyAdapter.adaptMany(raws));
    }

}
