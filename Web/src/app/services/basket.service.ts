import { Injectable } from "@angular/core";
import { Product } from "../models/product.model";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { Observable } from "rxjs";
import { BasketItem } from "../models/basket-item.model";

@Injectable({ providedIn: 'root' })
export class BasketService {
  private baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) { }

  getBasket(): Observable<BasketItem[]> {
    return this.http.get<BasketItem[]>(`${this.baseUrl}/Basket/basket`);
  }
  addToBasket(productId: number, quantity: number = 1): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/Basket/basket/${productId}?quantity=${quantity}`, {});
  }
  removeFromBasket(productId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/Basket/basket/${productId}`);
  }

}