import {Component, Inject} from "@angular/core";
import { CategoryRepository } from 'src/app/model/category.repository';
import {Category, CATEGORY_ID} from 'src/app/model/category/category.model';
import { ProductRepository } from 'src/app/model/product.repository';
import { Filter } from 'src/app/model/product/configClasses.repository';
import {Observable, Observer, Subject} from "rxjs";
import {Router} from "@angular/router";
import {CLEAR_EMITTER} from "../filter/filter.component";

@Component({
    selector: "spMenuPage",
    templateUrl: "menuPage.component.html"
})
export class MenuPageComponent{
    
    constructor(@Inject(CATEGORY_ID) private sentId: Observer<number>,
                private repository: CategoryRepository, 
                private products: ProductRepository,
                private filter: Filter, private router: Router,
                @Inject(CLEAR_EMITTER) private clearEmitter: Subject<void>) {
    }
    
    get categories(): Category[] {
        return this.repository.categories;
    }
    
    setFilterByCategory(category: Category){
        this.filter.reset();
        this.clearEmitter.next();
        this.filter.categoryId = category.id;
        this.repository.getCategory(category.id);
        this.products.getProducts();
        this.router.navigateByUrl(`/table/${category.nikName}`);
        this.sentId.next(category.id);
    }
}