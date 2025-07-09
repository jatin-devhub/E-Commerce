import { Component, inject, OnInit } from "@angular/core";
import { Product } from "../core/models/product.model";
import { ActivatedRoute } from "@angular/router";
import { BackendService } from "../core/services/backend.service";
import { MatCard, MatCardActions, MatCardContent, MatCardHeader, MatCardTitle } from "@angular/material/card";

@Component({
    selector: 'app-product-details',
    templateUrl: './product-details.component.html',
    styleUrl: './product-details.component.css',
    imports: [MatCard, MatCardHeader, MatCardContent, MatCardTitle, MatCardActions]
})
export class ProductDetailsComponent implements OnInit {
    private route = inject(ActivatedRoute);
    private api = inject(BackendService);

    product: Product | null = null;
    relatedProducts: Product[] = [];
    loading = true;
    error: string | null = null;

    ngOnInit(): void {
        const productId = this.route.snapshot.paramMap.get('id');
        if(productId){
            this.fetchProduct(Number(productId));
            this.fetchRelatedProducts(Number(productId));
        } else {
            this.error = 'Product id is incorrect'
        }
    }
    fetchProduct(id: number) {
        this.loading = true;
        this.api.get<Product>(`/products/${id}`).subscribe({
            next: (data) => {
                this.product = data;
                this.loading = false;
            },
            error: (err) => {
                console.error(err);
                this.error = 'Failed to load product';
                this.loading = false;
            }
        });
    }

    fetchRelatedProducts(id: number) {
        this.api.get<Product[]>(`/products/${id}/related`).subscribe({
            next: (data) => {
                this.relatedProducts = data;
            },
            error: (err) => {
                console.error(err);
            }
        });
    }

    addToCart(product: Product) {
    this.api.post('/cart/add', { productId: product.id, quantity: 1 })
      .subscribe({
        next: () => alert(`Added ${product.name} to cart.`),
        error: (err) => {
          console.error(err);
          alert('Failed to add to cart.');
        }
      });
  }
}