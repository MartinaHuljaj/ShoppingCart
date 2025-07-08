import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from "@angular/core";
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

  constructor(private basketService: BasketService, private cdr: ChangeDetectorRef) {}

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
        console.error('Error loading favorites:', err);
      }
    });
  }
    removeFromBasket(productId: number): void {
        this.basketService.removeFromBasket(productId).subscribe({
        next: () => {
            this.basketItems = this.basketItems.filter(item => item.id !== productId);
            this.cdr.markForCheck();
        },
        error: (err: any) => {
            console.error('Error removing product from cart:', err);
        }
        });
    }
}