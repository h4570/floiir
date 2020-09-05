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

    protected async post(invitationKey: string, reCaptchaToken: string, user: User): Promise<{ user: User, jwt: string }> {
        const uri = `user/`;
        const payload = { user, invitationKey, reCaptchaToken };
        console.log(JSON.stringify(payload));
        return this.http
            .post<any>(`${environment.urls.api}` + uri, payload, { observe: 'response' })
            .toPromise()
            .then(resp => {
                return { user: this.adapter.adapt(resp.body), jwt: resp.headers.get('x-auth-token') };
            });
    }

}
