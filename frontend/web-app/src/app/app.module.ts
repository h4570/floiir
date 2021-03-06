import './prototypes';
import { AppRoutingModule } from './app-routing.module';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { NavbarModule } from './components/navbar/navbar.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppService } from './services/app.service';
import { AuthService } from './services/auth.service';
import { HomeModule } from './components/pages/home/home.module';
import { RegisterModule } from './components/pages/register/register.module';
import { ConfirmEmailModule } from './components/pages/confirm-email/confirm-email.module';
import { SharedModule } from './components/shared/shared.module';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { environment } from 'src/environments/environment';
import { AuthGuard } from './auth-guard';
import { JwtModule } from '@auth0/angular-jwt';
import { MapModule } from './components/pages/map/map.module';
import { NavbarService } from './services/navbar.service';
import { AuthInterceptor } from './auth.interceptor';
import { I18nInterceptor } from './i18n.interceptor';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    HomeModule,
    MapModule,
    RegisterModule,
    ConfirmEmailModule,
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
    }),
    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem('auth-token')
      },
    })
  ],
  providers: [
    AppService,
    AuthService,
    AuthGuard,
    NavbarService,
    { provide: HTTP_INTERCEPTORS, useClass: I18nInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
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
