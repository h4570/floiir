import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class AuthService {

    constructor(
        private readonly jwtHelper: JwtHelperService
    ) { }

    public saveToken(token: string): void {
        localStorage.setItem('auth-token', token);
    }

    public get isAuthenticated(): boolean {
        if (this.token)
            return !this.jwtHelper.isTokenExpired(this.token);
        else return false;
    }

    private get token(): string | null {
        return localStorage.getItem('auth-token');
    }

}
