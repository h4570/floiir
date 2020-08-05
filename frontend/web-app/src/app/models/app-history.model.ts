export enum AppHistoryType {
    Add,
    Update,
    Delete
}

export enum AppTable {
    TableName = 0,
}

export interface AppHistory {
    id: number;
    tableId: AppTable;
    elementId: number;
    type: AppHistoryType;
    userId: number;
    userName: string;
    dateTime: Date;
    description: string;
}
