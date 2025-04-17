// trodha.client/src/app/_guards/auth.guard.ts
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const currentUser = this.authService.currentUserValue;
    if (currentUser) {
      // Kullanıcı giriş yapmış
      return true;
    }

    // Kullanıcı giriş yapmamış, login sayfasına yönlendir
    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    return false;
  }
}
