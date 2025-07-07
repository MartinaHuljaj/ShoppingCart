// product.service.ts

import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';
import { environment } from '../../environments/environment';

interface ProductResponse {
  products: Product[];
  limit: number;
  skip: number;
  total: number;
}

@Injectable({ providedIn: 'root' })
export class ProductService {
  private baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) { }

  getProducts(
    limit: number,
    skip: number,
    sortBy: string,
    order: 'asc' | 'desc'
  ): Observable<ProductResponse> {
    let params = new HttpParams()
      .set('limit', limit.toString())
      .set('skip', skip.toString())
      .set('sortBy', sortBy)
      .set('order', order);

    return this.http.get<ProductResponse>(`${this.baseUrl}/Product/products`, { params });
  }
}
