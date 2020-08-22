import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { UserHttpService } from './http/user.http.service';
import { User } from '../models/user.model';

export enum RegisterResponse {
    Success,
    FormInvalid,
    InvalidKey,
    KeyNotFound,
    KeyWasUsed,
    LoginIsUsed,
    EmailIsUsed,
    UnknownHttpError,
    UnknownError
}

@Injectable()
export class UserService extends UserHttpService {

    constructor(http: HttpClient) {
        super(http);
    }

    public async register(key: string, user: User): Promise<RegisterResponse> {
        return new Promise<RegisterResponse>(async (res) => {
            try {
                await this.post(key, user);
                res(RegisterResponse.Success);
            } catch (err) {
                if (err instanceof HttpErrorResponse) {
                    if (err.status === 410) res(RegisterResponse.InvalidKey);
                    else if (err.status === 420) res(RegisterResponse.KeyNotFound);
                    else if (err.status === 430) res(RegisterResponse.KeyWasUsed);
                    else if (err.status === 440) res(RegisterResponse.FormInvalid);
                    else if (err.status === 450) res(RegisterResponse.EmailIsUsed);
                    else if (err.status === 460) res(RegisterResponse.LoginIsUsed);
                    else res(RegisterResponse.UnknownHttpError);
                } else res(RegisterResponse.UnknownError);
            }
        });
    }

}
