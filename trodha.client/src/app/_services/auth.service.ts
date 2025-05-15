import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { AuthResponse, LoginRequest, RegisterRequest, User } from '../_models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<User | null>;
  public currentUser: Observable<User | null>;
  private apiUrl = 'http://localhost:5253/api/auth';
  constructor(private http: HttpClient) {
    const userJson = localStorage.getItem('currentUser');
    this.currentUserSubject = new BehaviorSubject<User | null>(userJson ? JSON.parse(userJson) : null);
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User | null {
    return this.currentUserSubject.value;
  }

  public get isLoggedIn(): boolean {
    return !!this.currentUserValue && !!localStorage.getItem('token');
  }

  // HTTP Options
  private getHttpOptions() {
    const token = localStorage.getItem('token');
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': token ? `Bearer ${token}` : ''
      })
    };
  }

  // Error handling
  private handleError(error: HttpErrorResponse) {
    let errorMessage = '';

    console.error('Raw API Error:', {
      status: error.status,
      statusText: error.statusText,
      url: error.url,
      message: error.message,
      error: error.error
    });

    // Client-side error
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Client Error: ${error.error.message}`;
    }
    // Backend error
    else {
      if (error.status === 0) {
        errorMessage = 'Sunucuya bağlanılamıyor. Lütfen internet bağlantınızı kontrol edin.';
      } else if (error.status === 400 && error.error) {
        // Custom handling for validation errors
        if (typeof error.error === 'string') {
          errorMessage = error.error;
        } else if (error.error.message) {
          errorMessage = error.error.message;
        } else if (error.error.errors) {
          const validationErrors = error.error.errors;
          const errors = [];

          // Extract validation errors
          for (const key in validationErrors) {
            if (validationErrors.hasOwnProperty(key)) {
              errors.push(`${validationErrors[key]}`);
            }
          }

          errorMessage = errors.join(' ');
        } else {
          errorMessage = 'Geçersiz istek. Lütfen bilgilerinizi kontrol edin.';
        }
      } else if (error.status === 401) {
        errorMessage = 'Yetkilendirme hatası. Lütfen tekrar giriş yapın.';
        this.logout();
      } else if (error.status === 404) {
        errorMessage = 'İstenilen kaynak bulunamadı.';
      } else if (error.status === 500) {
        errorMessage = 'Sunucu hatası. Lütfen daha sonra tekrar deneyin.';
      } else {
        errorMessage = `Sunucu Hatası: ${error.message}`;
      }
    }

    console.error('API Error:', error);
    return throwError(() => ({ message: errorMessage, originalError: error }));
  }

  register(registerRequest: RegisterRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/register`, registerRequest)
      .pipe(
        map(response => {
          if (response && response.token) {
            localStorage.setItem('currentUser', JSON.stringify(response.user));
            localStorage.setItem('token', response.token);
            this.currentUserSubject.next(response.user);
          }
          return response;
        }),
        catchError(this.handleError)
      );
  }

  login(loginRequest: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, loginRequest)
      .pipe(
        map(response => {
          if (response && response.token) {
            localStorage.setItem('currentUser', JSON.stringify(response.user));
            localStorage.setItem('token', response.token);
            this.currentUserSubject.next(response.user);
          }
          return response;
        }),
        catchError(this.handleError)
      );
  }

  logout() {
    localStorage.removeItem('currentUser');
    localStorage.removeItem('token');
    this.currentUserSubject.next(null);
  }

  forgotPassword(email: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/forgot-password`, { email })
      .pipe(catchError(this.handleError));
  }

  validateToken(): Observable<boolean> {
    return this.http.get<boolean>(`${this.apiUrl}/validate-token`, this.getHttpOptions())
      .pipe(
        catchError(() => {
          this.logout();
          return throwError(() => false);
        })
      );
  }

}

