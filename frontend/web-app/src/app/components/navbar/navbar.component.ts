import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { NavbarService } from '../../services/navbar.service';
import { trigger, transition, style, animate } from '@angular/animations';

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
          animate('0.2s ease-out',
            style({ height: 80 }))
        ]
      ),
      transition(
        ':leave',
        [
          style({ opacity: 1 }),
          animate('0.2s ease-out',
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
    public nav: NavbarService
  ) { }

  public changeLang(language: string): void {
    localStorage.setItem('language', language);
    this.translate.use(language);
  }

}
