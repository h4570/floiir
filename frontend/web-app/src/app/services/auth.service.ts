import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AuthService {

    constructor(
        private readonly http: HttpClient
    ) { }

    public saveToken(token: string): void {
        localStorage.setItem('auth-token', token);
    }

    public loadToken(): string {
        return localStorage.getItem('auth-token');
    }

}
