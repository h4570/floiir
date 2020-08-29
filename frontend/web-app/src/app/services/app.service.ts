import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable()
export class AppService {

    constructor(
        private readonly http: HttpClient
    ) { }

    public version: string;
    public build: string;

    public async setAppInfo(): Promise<void> {
        const appInfo = await this.http
            .get<{ version: string, build: string }>(environment.urls.api + 'app-info')
            .toPromise();
        this.version = appInfo.version;
        this.build = appInfo.build;
    }

    public get appTerms(): Promise<string> {
        return this.http
            .get<string>('assets/terms.txt', { responseType: 'text' as 'json' })
            .toPromise()
            .then(result => result as any as string);
    }

}
