import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
    selector: 'app-header',
    standalone: true,
    imports: [MatToolbarModule, MatIconModule, MatButtonModule, CommonModule],
    templateUrl: './app-header.component.html',
    styleUrls: ['./app-header.component.less'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppHeaderComponent implements OnInit {
    isAuthenticated = false;
    constructor(private router: Router, private auth: AuthService, private cdr: ChangeDetectorRef) { }
    ngOnInit() {
        this.auth.loggedIn$.subscribe(status => {
            this.isAuthenticated = status;
            this.cdr.markForCheck();
        });
    }
    goToProducts() {
        this.router.navigate(['']);
    }
    onLogin() {
        this.router.navigate(['/login']);
    }

    onViewFavorites() {
        this.router.navigate(['/favorites']);
    }

    onViewBasket() {
        this.router.navigate(['/basket']);
    }

    onLogout() {
        this.auth.logout();
        this.router.navigate(['']);
    }
}
