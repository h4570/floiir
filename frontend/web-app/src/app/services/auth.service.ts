import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthHttpService } from './http/auth.http.service';

export enum LoginResponse {
    Success,
    FormInvalid,
    UserNotFound,
    ReCaptchaError,
    UnknownHttpError,
    UnknownError
}

@Injectable()
export class AuthService extends AuthHttpService {

    constructor(
        private readonly jwtHelper: JwtHelperService,
        http: HttpClient
    ) {
        super(http);
    }

    /** Set's JWT auth token in localStorage */
    public saveToken(token: string): void {
        localStorage.setItem('auth-token', token);
    }

    /** Check's if auth token exists and if is not expired */
    public get isAuthenticated(): boolean {
        if (this.token)
            return !this.jwtHelper.isTokenExpired(this.token);
        else return false;
    }

    /** Returns auth token from localStorage */
    public get token(): string | null {
        return localStorage.getItem('auth-token');
    }

    /** Login user in floiir. If call is successfull JWT token is saved in local storage */
    public async login(recaptcha: string, login: string, password: string): Promise<LoginResponse> {
        return new Promise<LoginResponse>(async (res) => {
            try {
                await this.post(recaptcha, login, password);
                res(LoginResponse.Success);
            } catch (err) {
                if (err instanceof HttpErrorResponse) {
                    if (err.status === 461) res(LoginResponse.UserNotFound);
                    else if (err.status === 499) res(LoginResponse.ReCaptchaError);
                    else res(LoginResponse.UnknownHttpError);
                } else res(LoginResponse.UnknownError);
            }
        });
    }


}
