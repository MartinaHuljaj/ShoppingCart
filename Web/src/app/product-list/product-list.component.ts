import { ChangeDetectionStrategy, ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../services/product.service';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { BehaviorSubject } from 'rxjs';
import { Product } from '../models/product.model';
import { FormsModule } from '@angular/forms';
import { FavoritesService } from '../services/favorites.service';
import { BasketService } from '../services/basket.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../services/auth.service';

@Component({
    selector: 'app-product-list',
    standalone: true,
    imports: [
        MatCardModule,
        MatPaginatorModule,
        MatSortModule,
        MatTableModule,
        MatSelectModule,
        MatFormFieldModule,
        MatInputModule,
        MatProgressSpinnerModule,
        MatButtonModule,
        MatIconModule,
        CommonModule,
        FormsModule],
    templateUrl: './product-list.component.html',
    styleUrls: ['./product-list.component.less'],
    changeDetection: ChangeDetectionStrategy.OnPush,

})
export class ProductListComponent implements OnInit {
    products$ = new BehaviorSubject<any[]>([]);
    total$ = new BehaviorSubject<number>(0);
    private snackBar = inject(MatSnackBar);
    limit = 10;
    skip = 0;
    total = 0;
    pageIndex = 0;

    sortBy = 'id';
    order: 'asc' | 'desc' = 'asc';
    isAuthenticated = false;

    constructor(private productService: ProductService, private favoritesService: FavoritesService, private BasketService: BasketService, private auth: AuthService, private cdr: ChangeDetectorRef) { }

    ngOnInit(): void {
        this.auth.loggedIn$.subscribe(status => {
            this.isAuthenticated = status;
            this.cdr.markForCheck();
        });
        this.fetchProducts();
    }

    fetchProducts(): void {
        this.productService
            .getProducts(this.limit, this.skip, this.sortBy, this.order)
            .subscribe((res: any) => {
                this.products$.next(res.products);
                this.total$.next(res.total || 0);
                this.cdr.detectChanges();
            });
    }

    onPageChange(event: any): void {
        this.limit = event.pageSize;
        this.skip = event.pageIndex * event.pageSize;
        this.pageIndex = event.pageIndex;
        this.fetchProducts();
    }

    onSortChange(): void {
        this.skip = 0;
        this.pageIndex = 0;
        this.fetchProducts();
    }

    addToBasket(product: Product, quantity: number): void {
        this.BasketService.addToBasket(product.id, quantity).subscribe({
            next: (res: any) => {
                this.snackBar.open(res?.Message || 'Product successfully added to basket', 'Close', {
                    duration: 3000,
                    panelClass: ['snackbar-success'],
                    horizontalPosition: 'center',
                    verticalPosition: 'top'
                });
            },
            error: (err: any) => {
                this.snackBar.open(err?.Message || 'Error while adding to basket', 'Close', {
                    duration: 3000,
                    panelClass: ['snackbar-error'],
                    horizontalPosition: 'center',
                    verticalPosition: 'top'
                });
            }
        });
    }
    addToFavorites(product: Product): void {
        this.favoritesService.addToFavorites(product.id).subscribe({
            next: (res: any) => {
                this.snackBar.open(res?.Message || 'Product successfully added to favorites.', 'Close', {
                    duration: 3000,
                    panelClass: ['snackbar-success'],
                    horizontalPosition: 'center',
                    verticalPosition: 'top'
                });
            },
            error: (err) => {
                this.snackBar.open(err?.Message || 'Error while adding to favorites', 'Close', {
                    duration: 3000,
                    panelClass: ['snackbar-error'],
                    horizontalPosition: 'center',
                    verticalPosition: 'top'
                });
            }
        });
    }
}
