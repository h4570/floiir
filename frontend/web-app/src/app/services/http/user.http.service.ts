import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from './../../../environments/environment';
import { UserAdapter } from './../../adapters/user.adapter';
import { User } from './../../models/user.model';

@Injectable()
export class UserHttpService {

    private readonly adapter: UserAdapter;

    constructor(
        private readonly http: HttpClient
    ) {
        this.adapter = new UserAdapter();
    }

    protected async post(invKey: string, user: User): Promise<{ user: User, jwt: string }> {
        const uri = `user/${encodeURIComponent(invKey)}`;
        return this.http
            .post<any>(`${environment.urls.api}` + uri, user, { observe: 'response' })
            .toPromise()
            .then(resp => {
                return { user: this.adapter.adapt(resp.body), jwt: resp.headers.get('x-auth-token') };
            });
    }

}
