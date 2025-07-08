import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from "@angular/core";
import { Product } from "../models/product.model";
import { FavoritesService } from "../services/favorites.service";
import { MatCardModule } from "@angular/material/card";
import { CommonModule } from "@angular/common";
import { MatButtonModule } from "@angular/material/button";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { MatSelectModule } from "@angular/material/select";
import { MatSortModule } from "@angular/material/sort";
import { MatTableModule } from "@angular/material/table";

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
        CommonModule],
    templateUrl: './favorites.component.html',
    styleUrls: ['./favorites.component.less'],
    changeDetection: ChangeDetectionStrategy.OnPush,

})
export class FavoritesComponent implements OnInit {
    favorites: Product[] = [];

    constructor(private favoritesService: FavoritesService, private cdr: ChangeDetectorRef) { }

    ngOnInit(): void {
        this.loadFavorites();
    }

    private loadFavorites(): void {
        this.favoritesService.getFavorites().subscribe({
            next: (products) => {
                this.favorites = products;
                this.cdr.markForCheck();
            },
            error: (err) => {
                console.error('Error loading favorites:', err);
            }
        });
    }

    removeFromFavorites(productId: number): void {
        this.favoritesService.removeFromFavorites(productId).subscribe({
            next: () => {
                const updated = this.favorites.filter(item => item.id !== productId);
                this.favorites = [...updated];
                this.cdr.markForCheck();

            },
            error: err => {
                console.error('Error removing from favorites:', err);
            }
        });
    }
}