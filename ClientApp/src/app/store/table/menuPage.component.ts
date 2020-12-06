import {Component} from "@angular/core";
import {Category} from "../../model/category/category.model";
import {CategoryRepository} from "../../model/category.repository";
import {ProductRepository} from "../../model/product.repository";

@Component({
    selector: "spMenuPage",
    templateUrl: "menuPage.component.html"
})
export class MenuPageComponent{
    
    constructor(private repositories: CategoryRepository, private products: ProductRepository) {
    }
    
    get categories(): Category[] {
        return this.repositories.categories;
    }
    
    setFilterByCategory(categoryId: number){
        this.products.filter.categoryId = categoryId;
        this.products.getProducts();
    }
}