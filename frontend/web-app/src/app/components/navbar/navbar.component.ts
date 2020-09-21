import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { NavbarService } from '../../services/navbar.service';
import { trigger, transition, style, animate } from '@angular/animations';
import { MatDialog } from '@angular/material/dialog';
import { LoginDialogComponent } from '../shared/login-dialog/login-dialog.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  animations: [
    trigger('inOutAnimation', [
      transition(
        ':enter',
        [
          style({ opacity: 0 }),
          animate('0.4s ease-out',
            style({ opacity: 1, height: 90 })),
          animate('0.4s ease-in',
            style({ height: 40 }))
        ]
      ),
      transition(
        ':leave',
        [
          style({ opacity: 1 }),
          animate('0.4s ease-out',
            style({ height: 80 })),
          animate('0.4s ease-in',
            style({ opacity: 0, height: 0 }))
        ]
      )
    ]
    )
  ]
})
export class NavbarComponent {

  constructor(
    public readonly translate: TranslateService,
    public nav: NavbarService,
    public dialog: MatDialog
  ) { }

  public changeLang(language: string): void {
    localStorage.setItem('language', language);
    this.translate.use(language);
  }
  public logIn(): void {
    const dialogRef = this.dialog.open(LoginDialogComponent, {
      width: '400px',
      data: {},
      position: { top: '100px' }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

}
