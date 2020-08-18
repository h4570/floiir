import { User } from './user.model';

export interface InvitationKey {

    /** Primary key */
    id: number;

    /** Max length: 10, non-nullable */
    key: string;

    /** Non-nullable */
    inviterId: number;

    /** Non-nullable, included in backend based on inviterId */
    inviter: User;

    /** Non-nullable, generated in backend */
    createdAt: Date;

    /** Nullable */
    usedByUserId: number;

    /** Nullable, included in backend based on usedByUserId */
    usedByUser: User;

}
