import { Component, OnInit, signal } from '@angular/core';
import { Product } from '../../models/product.model';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-product-detail',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './product-detail.html',
  styleUrl: './product-detail.scss'
})
export class ProductDetail implements OnInit {
  product = signal<Product>(null);
  editMode = signal(false);

  productForm = new FormGroup({
    name: new FormControl('', { nonNullable: true, validators: [Validators.required] })
  });

  constructor(private route: ActivatedRoute, private productService: ProductService) { }

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id) {
      this.productService.getProduct(id).subscribe(data => {
        this.product.set(data);
        this.productForm.patchValue({
          name: data.name
        });
      });
    }
  }

  toggleEdit() {
    this.editMode.update(v => !v);
  }

  saveChanges() {
    if (this.productForm.valid) {
      const updatedData = this.productForm.getRawValue(); 
      this.product.update(u => ({ ...u, ...updatedData }));
      this.editMode.set(false);
    }
  }
}
