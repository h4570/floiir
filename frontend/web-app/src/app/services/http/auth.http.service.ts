import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { UserAdapter } from '../../adapters/user.adapter';
import { User } from '../../models/user.model';
import { UserLoginDto } from 'src/app/dtos/user-login.dto';

@Injectable()
export class AuthHttpService {

    private readonly adapter: UserAdapter;

    constructor(
        private readonly http: HttpClient
    ) {
        this.adapter = new UserAdapter();
    }

    protected async post(reCaptchaToken: string, login: string, password: string): Promise<UserLoginDto> {
        const uri = `auth/`;
        const payload = { login, password, reCaptchaToken };
        console.log(environment.urls.api + uri);
        return this.http
            .post<any>(`${environment.urls.api}` + uri, payload)
            .toPromise()
            .then(resp => this.adapter.adapt(resp.body));
    }

}
