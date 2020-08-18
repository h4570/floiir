import './prototypes';
import { AppRoutingModule } from './app-routing.module';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { NavbarModule } from './components/navbar/navbar.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AppService } from './services/app.service';
import { HomeModule } from './components/pages/home/home.module';
import { RegisterModule } from './components/pages/register/register.module';
import { SharedModule } from './components/shared/shared.module';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    HomeModule,
    RegisterModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    SharedModule,
    NavbarModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [
    AppService
  ],
  bootstrap: [
    AppComponent
  ], entryComponents: [

  ]
})
export class AppModule { }

// required for AOT compilation
// tslint:disable-next-line: typedef
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, `${environment.urls.app}assets/i18n/`);
}
