import { Component, OnInit } from '@angular/core';
import { Category } from './models/category';
import { ProductDto } from './models/productDto';
import { ProductService } from './services/product.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  products: ProductDto[];
  categories: Category[];
  productForm: boolean;
  isNewProduct: boolean;
  newProduct: ProductDto;
  editProductForm: boolean;
  editedProduct: ProductDto;
  isNewCategory: boolean;
  newCategory: Category;
  categoryForm: boolean;
  isOpenAlert: boolean;
  alertMessage: string;

  constructor(private productService: ProductService) { }

  ngOnInit() {
    
    this.newCategory = new Category();
    this.newProduct = new ProductDto();
    this.loadProductsList();
    this.loadCategories();
  }

  showEditProductForm(product: ProductDto) {
    this.editProductForm = true;
    this.productForm = false;
    this.categoryForm = false;
    this.editedProduct = product;
    this.editedProduct.category = this.categories.find(x => x.categoryID == this.editedProduct.category.categoryID);
  }

  showAddProductForm() {
    this.productForm = true;
    this.isNewProduct = true;
    this.categoryForm = false;
    this.editProductForm = false;
  }

  saveProduct(product: ProductDto) {
    if(product.category.categoryID == undefined){
      this.alertMessage = "Select a category";
      this.isOpenAlert = true;
      return;
    }

    if (this.isNewProduct) {
       this.productService.addProduct(product).subscribe(x =>{
        this.alertMessage = "Product was added successfully";
        this.isOpenAlert = true;
        this.loadProductsList();
        this.cancelNewProduct();
       });
    }
    this.productForm = false;
  }

  updateProduct() {
    this.productService.updateProduct(this.editedProduct).subscribe(
      x => {
        this.alertMessage = "Product was updated successfully";
        this.isOpenAlert = true;
        this.loadProductsList();
        this.cancelEdits();
      }
    )
  }

  removeProduct(product: ProductDto) {
    this.productService.deleteProduct(product).subscribe(
      x=> {
        this.alertMessage = "Product was deleted successfully";
        this.isOpenAlert = true;
        this.loadProductsList();
      }
    )
  }

  cancelEdits() {
    this.editedProduct = new ProductDto();
    this.editProductForm = false;
  }

  cancelNewProduct() {
    this.newProduct = new ProductDto();
    this.productForm = false;
  }

  showAddCategoryForm() {
    this.categoryForm = true;
    this.isNewCategory = true;
    this.productForm = false;
    this.editProductForm = false;
  }

  cancelCategory(){
    this.newCategory = new Category();
    this.categoryForm = false;
  }

  saveCategory(category: Category){
    this.productService.addCategory(category).subscribe(
      x => {
        this.alertMessage = "Category was added successfully";
        this.isOpenAlert = true;
        this.cancelCategory();  
        this.loadCategories();
      }
    );
  }

  loadCategories() {
    this.productService.getCategories().subscribe(categories => {
      this.categories = categories;
    })
  }

  loadProductsList(){
    this.productService.getProducts().subscribe(products => {
      this.products = products;
    })
  }
}
