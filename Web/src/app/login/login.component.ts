import { Component, ChangeDetectionStrategy, ChangeDetectorRef, inject } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule, FormGroup } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CommonModule } from '@angular/common';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
    selector: 'app-login',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatCardModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatProgressSpinnerModule
    ],
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.less'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent {
    loading = false;
    error: string = '';

    form: FormGroup;
    private snackBar = inject(MatSnackBar);

    constructor(
        private fb: FormBuilder,
        private auth: AuthService,
        private router: Router,
        private cdr: ChangeDetectorRef
    ) {
        this.form = this.fb.group({
            email: ['', [Validators.required, Validators.email]],
            password: ['', Validators.required]
        });
    }

    onSubmit() {
        if (this.form.invalid) return;

        this.loading = true;
        this.error = '';

        const { email, password } = this.form.value;
        this.auth.login(email!, password!).subscribe({
            next: (res: any) => {
                this.loading = false;
                this.router.navigateByUrl('/');
                this.cdr.detectChanges();
                this.snackBar.open(res?.Message || 'Login successful', 'Close', {
                    duration: 3000,
                    panelClass: ['snackbar-success'],
                    horizontalPosition: 'center',
                    verticalPosition: 'top'
                });
            },
            error: err => {
                this.loading = false;
                this.error = err.error?.message || 'Login failed';
                this.snackBar.open(this.error, 'Close', {
                    duration: 3000,
                    panelClass: ['snackbar-error'],
                    horizontalPosition: 'center',
                    verticalPosition: 'top'
                });
            }
        });
    }

    goToRegisterPage() {
        this.router.navigateByUrl('/register');
    }
}
