import { Component, signal } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-product-list',
  imports: [CommonModule],
  templateUrl: './product-list.html',
  styleUrl: './product-list.scss'
})
export class ProductList {
  products = signal<Product[]>([]);
  pageSize = 1; // Default page size
  searchParams = {
    pageNumber: 1,
    pageSize: this.pageSize,
    sortOrders: {
    },
    searchFilters: {
    }
  };

  constructor(private productService: ProductService, private router: Router) { }

  ngOnInit() {
    this.productService.getProducts(this.searchParams)
      .subscribe(pageResult => {
          this.products.set(pageResult.items);
        });
  }

  goToDetail(id: number) {
    this.router.navigate(['/products', id]);
  }
}
