import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, tap } from "rxjs";

@Injectable({ providedIn: 'root' })
export class AuthService {
  isLoggedIn$ = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) {
    const token = localStorage.getItem('jwt');
    this.isLoggedIn$.next(!!token);
  }

  login(credentials: { email: string; password: string }): Observable<any> {
    return this.http.post<{ token: string }>(`auth/login`, credentials)
      .pipe(
        tap(response => {
          localStorage.setItem('jwt', response.token);
          this.isLoggedIn$.next(true);
        })
      );
  }

  logout() {
    localStorage.removeItem('jwt');
    this.isLoggedIn$.next(false);
  }
}