import { ChangeDetectionStrategy, ChangeDetectorRef, Component, inject, OnInit } from "@angular/core";
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
import { BasketItem } from "../models/basket-item.model";
import { BasketService } from "../services/basket.service";
import { MatSnackBar } from "@angular/material/snack-bar";

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
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,

})
export class BasketComponent implements OnInit {
  basketItems: BasketItem[] = [];
  private snackBar = inject(MatSnackBar);

  constructor(private basketService: BasketService, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.loadBasket();
  }

  private loadBasket(): void {
    this.basketService.getBasket().subscribe({
      next: (products: BasketItem[]) => {
        this.basketItems = products;
        this.cdr.markForCheck();
      },
      error: (err: any) => {
        this.snackBar.open('Failed to load basket. Please try again later.', 'Close', {
          duration: 3000,
          panelClass: ['snackbar-error'],
          horizontalPosition: 'center',
          verticalPosition: 'top'
        });
      }
    });
  }
  removeFromBasket(productId: number): void {
    this.basketService.removeFromBasket(productId).subscribe({
      next: (res: any) => {
        this.basketItems = this.basketItems.filter(item => item.id !== productId);
        this.cdr.markForCheck();
        this.snackBar.open(res?.Message || 'Removed from favorites.', 'Close', {
          duration: 3000,
          panelClass: ['snackbar-success'],
          horizontalPosition: 'center',
          verticalPosition: 'top'
        });
      },
      error: (err: any) => {
        this.snackBar.open(err.Message || 'Error while removing from basket', 'Close', {
          duration: 3000,
          panelClass: ['snackbar-error'],
          horizontalPosition: 'center',
          verticalPosition: 'top'
        });
      }
    });
  }
}