import { Adapter } from './adapter';
import { User } from '../models/user.model';
import { InvitationKey } from '../models/invitation-key.model';

export class InvitationKeyAdapter extends Adapter<InvitationKey> {

    private readonly userAdapter: Adapter<User>;

    constructor(userAdapter: Adapter<User>) {
        super();
        this.userAdapter = userAdapter;
    }

    adapt(raw: InvitationKey): InvitationKey {
        raw.createdAt = new Date(raw.createdAt);
        if (raw.inviter)
            raw.inviter = this.userAdapter.adapt(raw.inviter);
        if (raw.usedByUser)
            raw.usedByUser = this.userAdapter.adapt(raw.usedByUser);
        return raw;
    }

}
