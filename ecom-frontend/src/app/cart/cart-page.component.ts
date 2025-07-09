import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { BackendService } from '../core/services/backend.service';
import { CartItem } from '../core/models/cart-item.model';

interface CartResponse {
    items: CartItem[],
    cartTotal: number
}

@Component({
  selector: 'app-cart-page',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
  ],
  styleUrl: './cart-page.component.css',
  templateUrl: './cart-page.component.html',
})
export class CartPageComponent implements OnInit {
  private api = inject(BackendService);
  private fb = inject(FormBuilder);
  private cdr = inject(ChangeDetectorRef);

  cartItems: CartItem[] = [];
  forms: { [id: number]: FormGroup } = {};
  loading = true;
  error: string | null = null;

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart(): void {
    this.api.get<CartResponse>('/cart')
      .subscribe({
        next: (data) => {
          this.cartItems = data.items;
          this.forms = {};
          for (let item of data.items) {
            this.forms[item.id] = this.fb.group({
              quantity: [item.quantity, [Validators.required, Validators.min(1)]]
            });
          }
          this.loading = false;
          this.cdr.markForCheck();
        },
        error: (err) => {
          this.error = 'Failed to load cart.';
          console.error(err);
          this.loading = false;
          this.cdr.markForCheck();
        }
      });
  }

  updateQuantity(item: CartItem): void {
    const form = this.forms[item.id];
    if (form.invalid) {
      form.markAllAsTouched();
      return;
    }
    const quantity = form.value.quantity;
    console.log(quantity, item)
    this.api.put(`/Cart/${item.id}`, { quantity })
      .subscribe({
        next: () => alert('Quantity updated!'),
        error: (err) => {
          console.error(err);
          alert('Failed to update quantity');
        }
      });
  }

  removeItem(itemId: number): void {
    this.api.delete(`/cart/${itemId}`)
      .subscribe({
        next: () => {
          this.cartItems = this.cartItems.filter(i => i.id !== itemId);
          delete this.forms[itemId];
        },
        error: (err) => {
          console.error(err);
          alert('Failed to remove item');
        }
      });
  }

  getTotal(): number {
    return this.cartItems.reduce((acc, item) => acc + item.unitPrice * item.quantity, 0);
  }
}
