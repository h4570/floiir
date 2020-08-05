import { Component, OnInit } from '@angular/core';
import { AppService } from './services/app.service';
import { TranslateService } from '@ngx-translate/core';

export const DEFAULT_LANG = 'en-US';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  private currentLang = DEFAULT_LANG;

  constructor(
    public readonly appService: AppService,
    private readonly translate: TranslateService,
  ) { }

  public async ngOnInit(): Promise<any> {
    await this.appService.setAppInfo();
    await this.configureLanguage();
  }

  private async configureLanguage(): Promise<any> {
    this.tryToGetUserLanguage();
    this.translate.setDefaultLang(this.currentLang);
    await this.translate.use(this.currentLang).toPromise();
  }

  private tryToGetUserLanguage(): void {
    const userLang = localStorage.getItem('language');
    this.currentLang = userLang ? userLang : this.currentLang;
  }

}
