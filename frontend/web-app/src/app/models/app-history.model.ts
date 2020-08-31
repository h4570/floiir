import { User } from './user.model';

export enum AppHistoryType {
    Add,
    Update,
    Delete
}

export enum AppTable {
    TableName = 0,
}

export interface AppHistory {

    /** Primary key */
    id: number;

    /** Non-nullable */
    tableId: AppTable;

    /** Nullable */
    elementId: number;

    /** Non-nullable */
    type: AppHistoryType;

    /** Non-nullable */
    userId: number;

    /** Non-nullable, included in backend based on userId */
    user: User;

    /** Non-nullable */
    dateTime: Date;

    /** Max length: 200, nullable */
    description: string;

}
