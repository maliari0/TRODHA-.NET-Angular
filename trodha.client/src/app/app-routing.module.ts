import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { ForgotPasswordComponent } from './auth/forgot-password/forgot-password.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProfileComponent } from './profile/profile.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  { path: '', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },

  {
    path: 'goals',
    loadChildren: () => import('./goals/goals.module').then(m => m.GoalsModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'notes',
    loadChildren: () => import('./notes/notes.module').then(m => m.NotesModule),
    canActivate: [AuthGuard]
  },

  // Bulunamayan sayfalar için wildcard route
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
