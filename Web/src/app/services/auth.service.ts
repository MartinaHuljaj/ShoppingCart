import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap, map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginResponse } from '../models/login-response.model';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private baseUrl = environment.apiBaseUrl;
    private readonly TOKEN_KEY = 'jwt_token';
    private getLocalStorage(): Storage | null {
        return typeof window !== 'undefined' ? window.localStorage : null;
    }

    private loggedInSubject = new BehaviorSubject<boolean>(!!this.getLocalStorage()?.getItem(this.TOKEN_KEY));



    constructor(private http: HttpClient) { }

    login(email: string, password: string): Observable<void> {
        return this.http
            .post<LoginResponse>(`${this.baseUrl}/Auth/login`, { email, password })
            .pipe(
                tap(res => this.getLocalStorage()?.setItem(this.TOKEN_KEY, res.token)),
                map(() => void 0)
            );
    }

    token(): string | null {
        const token = this.getLocalStorage()?.getItem(this.TOKEN_KEY);
        return typeof token === 'undefined' ? null : token;
    }

    isLoggedIn(): boolean {
        const token = this.token();
        return token !== null && token !== '';
    }
}
