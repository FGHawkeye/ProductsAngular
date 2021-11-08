import { Category } from "./category";
import { Product } from "./product";

export class ProductDto {
    product: Product;
    category: Category;

    constructor(){
        this.product = new Product();
        this.category = new Category();
    }
}
