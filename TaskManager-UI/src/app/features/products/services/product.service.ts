import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';
import { PageResult } from '../../../shared/models/page-result';

@Injectable({ providedIn: 'root' })
export class ProductService {
  constructor(private http: HttpClient) {}

  getProducts(params: any): Observable<PageResult<Product>> {
    return this.http.post<PageResult<Product>>('projects/search', params); // interceptor will prepend base URL
  }

  getProduct(id: number): Observable<Product> {
    return this.http.get<Product>(`projects/${id}`);
  }
}
