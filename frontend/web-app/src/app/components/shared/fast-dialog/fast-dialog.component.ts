import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TranslateService } from '@ngx-translate/core';

export enum DialogButtonType {
  YesNo,
  Ok,
  Confirm
}

export enum DialogType {
  Info,
  Alert,
  Error
}

@Component({
  selector: 'app-fast-dialog',
  templateUrl: './fast-dialog.component.html',
  styleUrls: ['./fast-dialog.component.scss']
})

export class FastDialogComponent {

  public DialogButtonType = DialogButtonType;
  public DialogType = DialogType;

  constructor(
    public readonly dialogRef: MatDialogRef<FastDialogComponent>,
    public readonly translate: TranslateService,
    @Inject(MAT_DIALOG_DATA) public payload: { type: DialogType, btnType: DialogButtonType, title: string, texts: string[] }) { }

}
