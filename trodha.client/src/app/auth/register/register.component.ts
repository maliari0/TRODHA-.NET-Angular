import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, AbstractControl, ValidatorFn } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../_services/auth.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs';

// Complex password validator function
export function createPasswordStrengthValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: boolean } | null => {
    const value = control.value;

    if (!value) {
      return null;
    }

    const hasUpperCase = /[A-Z]+/.test(value);
    const hasLowerCase = /[a-z]+/.test(value);
    const hasNumeric = /[0-9]+/.test(value);
    const hasSpecialChar = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/.test(value);

    const passwordValid = hasUpperCase && hasLowerCase && hasNumeric && hasSpecialChar;

    return !passwordValid ? { passwordStrength: true } : null;
  };
}

@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
  imports: [CommonModule, FormsModule, ReactiveFormsModule, RouterModule]
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  loading = false;
  submitted = false;
  error = '';

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService
  ) {
    if (this.authService.isLoggedIn) {
      this.router.navigate(['/']);
    }
  }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [
        Validators.required,
        Validators.minLength(8),
        createPasswordStrengthValidator()
      ]],
      confirmPassword: ['', Validators.required],
      birthDate: ['', Validators.required]
    }, {
      validators: this.passwordMatchValidator
    });
  }

  get f() { return this.registerForm.controls; }

  passwordMatchValidator(formGroup: FormGroup) {
    const password = formGroup.get('password')?.value;
    const confirmPassword = formGroup.get('confirmPassword')?.value;

    if (password !== confirmPassword) {
      formGroup.get('confirmPassword')?.setErrors({ passwordMismatch: true });
    } else {
      formGroup.get('confirmPassword')?.setErrors(null);
    }
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.registerForm.invalid) {
      return;
    }

    this.loading = true;

    const birthDateValue = this.f['birthDate'].value;
    let formattedBirthDate;

    if (birthDateValue instanceof Date) {
      formattedBirthDate = birthDateValue.toISOString().split('T')[0]; // YYYY-MM-DD formatı
    } else if (typeof birthDateValue === 'string') {
      formattedBirthDate = birthDateValue;
    } else {
      this.error = 'Invalid date format';
      this.loading = false;
      return;
    }

    const registerRequest = {
      firstName: this.f['firstName'].value,
      lastName: this.f['lastName'].value,
      email: this.f['email'].value,
      password: this.f['password'].value,
      birthDate: formattedBirthDate
    };


    this.authService.register(registerRequest)
      .pipe(first())
      .subscribe({
        next: () => {
          this.router.navigate(['/login'], { queryParams: { registered: 'true' } });
        },
        error: (error: any) => {
          if (error.originalError && error.originalError.error) {
            const backendError = error.originalError.error;
            if (typeof backendError === 'string') {
              this.error = backendError;
            } else if (backendError.errors) {
              const errors = [];
              for (const key in backendError.errors) {
                errors.push(backendError.errors[key]);
              }
              this.error = errors.join(' ');
            } else if (backendError.message) {
              this.error = backendError.message;
            } else {
              this.error = 'Kayıt işlemi başarısız';
            }
          } else {
            this.error = error.message || 'Kayıt işlemi başarısız';
          }
          this.loading = false;
        }
      });
  }
}
