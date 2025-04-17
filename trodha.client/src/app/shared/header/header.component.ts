// trodha.client/src/app/shared/header/header.component.ts
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../../_models/user.model';
import { AuthService } from '../../_services/auth.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  standalone: true,
  imports: [CommonModule, RouterModule]
})

export class HeaderComponent implements OnInit {
  currentUser: User | null = null;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {
    this.authService.currentUser.subscribe(user => this.currentUser = user);
  }

  ngOnInit(): void {
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
