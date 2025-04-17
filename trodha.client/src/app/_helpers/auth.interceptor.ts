import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private router: Router) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // Get token from local storage
    const token = localStorage.getItem('token');

    if (token) {
      // Clone the request and add the authorization header
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }

    // Handle the request and catch any errors
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        // Auto logout if 401 Unauthorized or 403 Forbidden response
        if (error.status === 401 || error.status === 403) {
          localStorage.removeItem('currentUser');
          localStorage.removeItem('token');
          this.router.navigate(['/login']);
        }

        return throwError(() => error);
      })
    );
  }
}

