import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfirmEmailComponent } from './confirm-email.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@NgModule({
  declarations: [
    ConfirmEmailComponent
  ],
  imports: [
    CommonModule,
    MatSnackBarModule,
  ]
})
export class ConfirmEmailModule { }
