import { Injectable } from "@angular/core";
import { Product } from "../models/product.model";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";
import { Observable } from "rxjs";

@Injectable({ providedIn: 'root' })
export class FavoritesService {
  private baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) { }

  getFavorites(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseUrl}/Favorites/favorites`);
  }
  addToFavorites(productId: number): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/Favorites/favorites/${productId}`, {});
  }
}