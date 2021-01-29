import {Component, Inject} from "@angular/core";
import { CategoryRepository } from 'src/app/model/category.repository';
import {Category, CATEGORY_ID} from 'src/app/model/category/category.model';
import { ProductRepository } from 'src/app/model/product.repository';
import { Filter } from 'src/app/model/product/configClasses.repository';
import {Observable, Observer} from "rxjs";

@Component({
    selector: "spMenuPage",
    templateUrl: "menuPage.component.html"
})
export class MenuPageComponent{
    
    constructor(@Inject(CATEGORY_ID) private sentId: Observer<number>,
                private repositories: CategoryRepository, 
                private products: ProductRepository,
                private filter: Filter) {
    }
    
    get categories(): Category[] {
        return this.repositories.categories;
    }
    
    setFilterByCategory(categoryId: number){
        this.filter.categoryId = categoryId;
        this.sentId.next(categoryId);
        this.products.getProducts();
    }
}