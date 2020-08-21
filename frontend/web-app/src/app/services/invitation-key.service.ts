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
export class InvitationKeyService extends InvitationKeyHttpService {

    constructor(http: HttpClient) {
        super(http);
    }

    public invitationKey: InvitationKey;
    public error: any;

    private keyCheckResponse: InvitationKeyGetResponse;
    private initialized: boolean;

    /** Checks invitation key and set's class vars. Could be used multiple times. */
    public async init(key: string): Promise<void> {
        return new Promise<void>(async (res) => {
            this.initialized = false;
            this.keyCheckResponse = undefined;
            this.invitationKey = undefined;
            try {
                this.invitationKey = await this.get(key);
                this.keyCheckResponse = InvitationKeyGetResponse.Success;
            } catch (err) {
                this.error = err;
                if (err instanceof HttpErrorResponse) {
                    if (err.status === 410) this.keyCheckResponse = InvitationKeyGetResponse.InvalidKey;
                    else if (err.status === 420) this.keyCheckResponse = InvitationKeyGetResponse.KeyNotFound;
                    else this.keyCheckResponse = InvitationKeyGetResponse.UnknownHttpError;
                } else this.keyCheckResponse = InvitationKeyGetResponse.UnknownError;
            }
            this.initialized = true;
            res();
        });
    }

    public get isError(): boolean {
        return this.initialized && (this.keyCheckResponse === InvitationKeyGetResponse.UnknownError ||
            this.keyCheckResponse === InvitationKeyGetResponse.UnknownHttpError);
    }

    public get isSuccess(): boolean {
        return this.initialized && this.keyCheckResponse === InvitationKeyGetResponse.Success;
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
