import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';

@Injectable()
export class I18nInterceptor implements HttpInterceptor {

    constructor(private readonly translate: TranslateService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        // Add language header in XX_xx format
        const language = this.translate.currentLang?.replace('-', '_');
        let newHeaders = req.headers;
        if (language) newHeaders = req.headers.append('User-Language', language);
        const langReq = req.clone({ headers: newHeaders });

        return next.handle(langReq);
    }

}
