import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { UserAdapter } from '../adapters/user.adapter';
import { User } from '../models/user.model';

@Injectable()
export class UserService {

    private readonly adapter: UserAdapter;

    constructor(
        private readonly http: HttpClient
    ) {
        this.adapter = new UserAdapter();
    }

    public async create(invKey: string, user: User): Promise<User> {
        const uri = `user/${encodeURIComponent(invKey)}`;
        return this.http
            .post<any>(`${environment.urls.api}` + uri, user)
            .toPromise()
            .then(raw => this.adapter.adapt(raw));
    }

}
