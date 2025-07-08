import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
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

    limit = 10;
    skip = 0;
    total = 0;
    pageIndex = 0;

    sortBy = 'id';
    order: 'asc' | 'desc' = 'asc';

    constructor(private productService: ProductService, private cdr: ChangeDetectorRef) { }

    ngOnInit(): void {
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

    addToCart(product: Product, quantity: number): void {
        console.log('Add to cart:', product, 'Quantity:', quantity);
    }
    addToFavorites(product: Product): void {
        console.log('Add to favorites:', product);
    }
}
