import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  inject,
  OnInit,
} from '@angular/core';
import { MatTreeModule } from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { BackendService } from '../core/services/backend.service';
import { Category } from '../core/models/category.model';
import { RouterModule } from '@angular/router';

/**
 * @title Tree with nested nodes (childrenAccessor)
 */
@Component({
  selector: 'app-category-navigation',
  templateUrl: './category-navigation.component.html',
  styleUrl: './category-navigation.component.css',
  imports: [MatTreeModule, MatButtonModule, MatIconModule, RouterModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CategoryNavigationComponent implements OnInit {
  private api = inject(BackendService);
  private cdr = inject(ChangeDetectorRef);
  childrenAccessor = (node: Category): Category[] => node?.children ?? [];

  dataSource: Category[] = [];

  hasChild = (_: number, node: Category) =>
    !!node.children && node.children.length > 0;
  loading = true;
  error: string | null = null;

  ngOnInit(): void {
    this.loading = true;
    this.error = null;
    this.api.get<Category[]>('/categories').subscribe({
      next: (data) => {
        console.log(data, "data")
        this.dataSource = [...data];
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: (err) => {
        this.error = 'Failed to load categories.';
        console.error(err);
        this.loading = false;
        this.cdr.markForCheck();
      },
    });
  }
}
