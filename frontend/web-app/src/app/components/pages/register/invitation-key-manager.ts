import { InvitationKeyService } from '../../../services/invitation-key.service';
import { InvitationKey } from '../../../models/invitation-key.model';
import { HttpErrorResponse } from '@angular/common/http';

enum InvitationKeyGetResponse {
    Success,
    InvalidKey,
    KeyNotFound,
    UnknownHttpError,
    UnknownError
}

/** Class which checks status of invitation key. Must be initialized through init() method */
export class InvitationKeyManager {

    constructor(invKeyService: InvitationKeyService) {
        this.service = invKeyService;
    }

    public invitationKey: InvitationKey;
    public error: any;

    private service: InvitationKeyService;
    private keyCheckResponse: InvitationKeyGetResponse;
    private loaded: boolean;

    /** Checks invitation key and set's class vars. Could be used multiple times. */
    public async init(key: string): Promise<void> {
        return new Promise<void>(async (res) => {
            this.loaded = false;
            this.keyCheckResponse = undefined;
            this.invitationKey = undefined;
            try {
                this.invitationKey = await this.service.get(key);
                this.keyCheckResponse = InvitationKeyGetResponse.Success;
            } catch (err) {
                this.error = err;
                if (err instanceof HttpErrorResponse) {
                    if (err.status === 410) this.keyCheckResponse = InvitationKeyGetResponse.InvalidKey;
                    else if (err.status === 420) this.keyCheckResponse = InvitationKeyGetResponse.KeyNotFound;
                    else this.keyCheckResponse = InvitationKeyGetResponse.UnknownHttpError;
                } else this.keyCheckResponse = InvitationKeyGetResponse.UnknownError;
            }
            this.loaded = true;
            res();
        });
    }

    public get isError(): boolean {
        return this.loaded && (this.keyCheckResponse === InvitationKeyGetResponse.UnknownError ||
            this.keyCheckResponse === InvitationKeyGetResponse.UnknownHttpError);
    }

    public get isSuccess(): boolean {
        return this.loaded && this.keyCheckResponse === InvitationKeyGetResponse.Success;
    }

    public get wasKeyNotFound(): boolean {
        return this.loaded && this.keyCheckResponse === InvitationKeyGetResponse.KeyNotFound;
    }

    public get isKeyInvalid(): boolean {
        return this.loaded && this.keyCheckResponse === InvitationKeyGetResponse.InvalidKey;
    }

    public get wasKeyUsed(): boolean {
        return this.loaded && this.invitationKey !== undefined &&
            this.invitationKey.usedByUserId !== null;
    }

}
