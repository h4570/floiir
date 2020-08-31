import { Injectable } from '@angular/core';
import { FastDialogComponent, DialogButtonType, DialogType } from './../components/shared/fast-dialog/fast-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Injectable()
export class FastDialogService {

    constructor(
        private readonly dialog: MatDialog,
    ) { }

    /** Fast API for creating and opening user dialog.
     * Return's 1 when: Ok, Yes, Confirm
     * Return's 0 when: No, Close
     */
    public open(type: DialogType, btnType: DialogButtonType, title: string, texts: string[]): Promise<0 | 1> {
        return new Promise<0 | 1>((res, rej) => {
            const dialogRef = this.dialog.open(FastDialogComponent, {
                maxWidth: '600px',
                data: { type, btnType, title, texts }
            });
            const sub = dialogRef.afterClosed().subscribe((result: boolean) => {
                sub.unsubscribe();
                res(result ? 1 : 0);
            });
        });
    }

}
