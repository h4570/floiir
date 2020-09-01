import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss']
})
export class ConfirmEmailComponent implements OnInit {

  constructor(
    private readonly snackBar: MatSnackBar,
  ) { }

  public ngOnInit(): void {
  }

  public async onResendEmailClick(): Promise<void> {
    const text = 'Email was sent again!'; // this.translate.instant('confirmEmail.emailResent') as string;
    this.snackBar.open(text, 'OK', { duration: 5000 });
  }


}
