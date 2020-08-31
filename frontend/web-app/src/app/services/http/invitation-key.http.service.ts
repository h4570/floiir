import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../../environments/environment';
import { InvitationKeyAdapter } from './../../adapters/invitation-key.adapter';
import { UserAdapter } from './../../adapters/user.adapter';
import { InvitationKey } from './../../models/invitation-key.model';

@Injectable()
export class InvitationKeyHttpService {

    private readonly adapter: InvitationKeyAdapter;

    constructor(
        private readonly http: HttpClient
    ) {
        const userAdapter = new UserAdapter();
        this.adapter = new InvitationKeyAdapter(userAdapter);
    }

    protected async get(key: string): Promise<InvitationKey> {
        const uri = `invitation-key/${encodeURIComponent(key)}`;
        return this.http
            .get<any>(`${environment.urls.api}` + uri)
            .toPromise()
            .then(raw => this.adapter.adapt(raw));
    }

}
