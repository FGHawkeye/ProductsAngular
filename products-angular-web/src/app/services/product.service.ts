import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Product } from '../models/product';
import { Observable } from 'rxjs';
import { Category } from '../models/category';
import { ProductDto } from '../models/productDto';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  baseUrl = `${environment.rootApi}/Products/`; 

  constructor(private httpClient: HttpClient) { }

  getProducts() : Observable<ProductDto[]>{
    return this.httpClient.get<ProductDto[]>(`${this.baseUrl}GetProducts`);
  }

  addCategory(category: Category){
    return this.httpClient.post<Category>(`${this.baseUrl}AddCategory`,category);
  }

  getCategories(): Observable<Category[]>{
    return this.httpClient.get<Category[]>(`${this.baseUrl}GetCategories`);
  }

  addProduct(product: ProductDto){
    return this.httpClient.post<ProductDto>(`${this.baseUrl}AddProduct`,product);
  }

  updateProduct(product: ProductDto){
    return this.httpClient.post<ProductDto>(`${this.baseUrl}UpdateProduct`, product);
  }

  deleteProduct(product: ProductDto){
    return this.httpClient.post<ProductDto>(`${this.baseUrl}DeleteProduct`, product);
  }
}
