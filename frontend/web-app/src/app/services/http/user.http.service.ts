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

    protected async post(invitationKey: string, reCaptchaToken: string, user: User): Promise<User> {
        const uri = `user/`;
        const payload = { user, invitationKey, reCaptchaToken };
        return this.http
            .post<any>(`${environment.urls.api}` + uri, payload)
            .toPromise()
            .then(resp => this.adapter.adapt(resp.body));
    }

}
