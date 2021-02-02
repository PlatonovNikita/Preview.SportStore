import {Component, Inject} from "@angular/core";
import {ActivatedRoute} from "@angular/router";
import {Filter} from "../../model/product/configClasses.repository";
import {CategoryRepository} from "../../model/category.repository";
import {CategoryRest} from "../../model/category.rest";
import {ProductRepository} from "../../model/product.repository";
import {Category, CATEGORY_ID} from "../../model/category/category.model";
import {Observer} from "rxjs";

@Component({
    selector: "spTable",
    templateUrl: "table.component.html"
})
export class TableComponent {
    _category: Category;
    
    constructor(private activeRoute: ActivatedRoute, public filter: Filter,
                private catRest: CategoryRest, private products: ProductRepository,
                private categories: CategoryRepository, @Inject(CATEGORY_ID) private sentId: Observer<number>) {
        activeRoute.params.subscribe(params => {
            if (params["category"] != null && params["category"] != undefined) {
                if (filter.categoryId == null) {
                    console.log(params["category"]);
                    this.catRest.getCategoryByNickName(params["category"])
                        .subscribe(cat => {
                            filter.categoryId = cat.id;
                            this.sentId.next(cat.id);
                            this.categories.getCategory(cat.id);
                            categories.newCategory.subscribe(() => this._category = categories.category);
                            this.products.getProducts();
                        });
                }
            }
        });
    }
    
    
    
    get category(): Category{
        return  this._category;
    }
}