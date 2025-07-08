import { Component, inject, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { BackendService } from '../core/services/backend.service';
import { Product } from '../core/models/product.model';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './product-list.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductListComponent implements OnInit {
  private api = inject(BackendService);
  private route = inject(ActivatedRoute);
  private fb = inject(FormBuilder);
  private cdr = inject(ChangeDetectorRef);

  products: Product[] = [];
  formGroups: { [productId: number]: FormGroup } = {};

  loading = true;
  error: string | null = null;

  ngOnInit(): void {
    const categoryId = Number(this.route.snapshot.paramMap.get('categoryId'));
    this.loadProducts(categoryId);
  }

  loadProducts(categoryId: number) {
    this.loading = true;
    this.error = null;

    this.api.get<Product[]>(`/products?categoryId=${categoryId}`)
      .subscribe({
        next: (data) => {
          this.products = data.filter(p => p.availableQty > 0);
          this.formGroups = {};
          this.products.forEach(p => {
            this.formGroups[p.id] = this.fb.group({
              quantity: [1, [
                Validators.required,
                Validators.min(1),
                Validators.max(p.availableQty)
              ]]
            });
          });
          this.loading = false;
          this.cdr.markForCheck();
        },
        error: (err) => {
          console.error(err);
          this.error = 'Failed to load products';
          this.loading = false;
          this.cdr.markForCheck();
        }
      });
  }

  addToCart(product: Product) {
    const form = this.formGroups[product.id];
    if (form.invalid) {
      form.markAllAsTouched();
      return;
    }
    const quantity = form.value.quantity;
    this.api.post('/cart/add', { productId: product.id, quantity })
      .subscribe({
        next: () => alert(`Added ${quantity} x ${product.name} to cart.`),
        error: (err) => {
          console.error(err);
          alert('Failed to add to cart.');
        }
      });
  }

  getQuantityControl(product: Product) {
    return this.formGroups[product.id].get('quantity')!;
  }
}
