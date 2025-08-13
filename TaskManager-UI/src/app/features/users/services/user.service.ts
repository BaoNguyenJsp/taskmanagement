import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { PageResult } from '../../../shared/models/page-result';

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private http: HttpClient) {}

  getUsers(params: any): Observable<PageResult<User>> {
    return this.http.post<PageResult<User>>('users/search', params); // interceptor will prepend base URL
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(`users/${id}`);
  }
}
