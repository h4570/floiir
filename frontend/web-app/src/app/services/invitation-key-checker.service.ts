import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { InvitationKeyHttpService } from './http/invitation-key.http.service';
import { InvitationKey } from '../models/invitation-key.model';

enum InvitationKeyGetResponse {
    Success,
    InvalidKey,
    KeyNotFound,
    UnknownHttpError,
    UnknownError
}

@Injectable()
export class InvitationKeyCheckerService extends InvitationKeyHttpService {

    constructor(http: HttpClient) {
        super(http);
    }

    public invitationKey: InvitationKey;
    public error: any;

    private keyCheckResponse: InvitationKeyGetResponse;
    private initialized: boolean;

    /** Checks invitation key and set's class vars. Could be used multiple times. */
    public async init(key: string): Promise<InvitationKeyGetResponse> {
        return new Promise<InvitationKeyGetResponse>(async (res) => {
            this.initialized = false;
            this.keyCheckResponse = undefined;
            this.invitationKey = undefined;
            try {
                this.invitationKey = await this.get(key);
                this.keyCheckResponse = InvitationKeyGetResponse.Success;
            } catch (err) {
                this.error = err;
                if (err instanceof HttpErrorResponse) {
                    if (err.status === 460) this.keyCheckResponse = InvitationKeyGetResponse.InvalidKey;
                    else if (err.status === 461) this.keyCheckResponse = InvitationKeyGetResponse.KeyNotFound;
                    else this.keyCheckResponse = InvitationKeyGetResponse.UnknownHttpError;
                } else this.keyCheckResponse = InvitationKeyGetResponse.UnknownError;
            }
            this.initialized = true;
            res(this.keyCheckResponse);
        });
    }

    /** True when key was checked and backend api returned 500 status code or there was another error  */
    public get isError(): boolean {
        return this.initialized && (this.keyCheckResponse === InvitationKeyGetResponse.UnknownError ||
            this.keyCheckResponse === InvitationKeyGetResponse.UnknownHttpError);
    }

    /** True when key was checked, exist is valid and was not used */
    public get isSuccess(): boolean {
        return this.initialized && this.keyCheckResponse === InvitationKeyGetResponse.Success && !this.wasKeyUsed;
    }

    public get wasKeyNotFound(): boolean {
        return this.initialized && this.keyCheckResponse === InvitationKeyGetResponse.KeyNotFound;
    }

    public get isKeyInvalid(): boolean {
        return this.initialized && this.keyCheckResponse === InvitationKeyGetResponse.InvalidKey;
    }

    public get wasKeyUsed(): boolean {
        return this.initialized && this.invitationKey !== undefined &&
            this.invitationKey.usedByUserId !== null;
    }

}
