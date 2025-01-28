import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Pagination } from '../../shared/models/pagination';
import { Product } from '../../shared/models/product';
import { ShopParams } from '../../shared/models/shopParams';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  types: string[] = [];
  brands: string[] = [];

  getProduct(id: number) {
    return this.http.get<Product>(this.baseUrl + 'products/' + id);
  }

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();

    // Brands
    if (shopParams.brands.length > 0) {
      params = params.append('brands', shopParams.brands.join(','));
    }

    // Types
    if (shopParams.types.length > 0) {
      params = params.append('types', shopParams.types.join(','));
    }

    // Sorting
    if (shopParams.sort) {
      params = params.append('sort', shopParams.sort);
    }

    // Search
    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    // PageSize (number of product requested)
    params = params.append('pageSize', shopParams.pageSize);

    // PageIndex
    params = params.append('pageIndex', shopParams.pageNumber);

    return this.http.get<Pagination<Product>>(this.baseUrl + 'products', {
      params,
    });
  }

  getBrands() {
    if (this.brands.length > 0) return;

    const brands = this.http
      .get<string[]>(this.baseUrl + `products/brands`)
      .subscribe({
        next: (response) => (this.brands = response),
      });
    return brands;
  }

  getTypes() {
    if (this.types.length > 0) return;

    const types = this.http
      .get<string[]>(this.baseUrl + `products/types`)
      .subscribe({
        next: (response) => (this.types = response),
      });
    return types;
  }
}
