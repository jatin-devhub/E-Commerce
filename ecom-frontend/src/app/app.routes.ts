import { Routes } from '@angular/router';
import { CategoryNavigationComponent } from './categories/category-navigation/category-navigation.component';
import { ProductListComponent } from './products/product-list.component';

export const routes: Routes = [
  { path: 'categories', component: CategoryNavigationComponent },
  { path: 'products/:categoryId', component: ProductListComponent },
  { path: '', redirectTo: '/categories', pathMatch: 'full' },
  { path: '**', redirectTo: '/categories' },
];
