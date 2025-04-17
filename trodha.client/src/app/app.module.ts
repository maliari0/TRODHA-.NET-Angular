import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

// components
import { LoginComponent } from './auth/login/login.component';
import { HeaderComponent } from './shared/header/header.component';
import { FooterComponent } from './shared/footer/footer.component';
import { DashboardComponent } from './dashboard/dashboard.component';

import { AuthInterceptor } from './_helpers/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    // Modüller
    BrowserModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    AppRoutingModule,

    LoginComponent,
    HeaderComponent,
    FooterComponent,
    DashboardComponent
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
