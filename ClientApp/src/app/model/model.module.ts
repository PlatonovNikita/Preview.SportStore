import {NgModule} from "@angular/core";
import {HttpClientModule} from "@angular/common/http";
import {ProductRepository} from "./product.repository";
import {CategoryRepository} from "./category.repository";
import {PRODUCT_URl, ProductRest} from "./product.rest";
import {CATEGORY_URL, CategoryRest} from "./category.rest";
import {Filter} from "./product/configClasses.repository";
import {CATEGORY_ID} from "./category/category.model";
import {Subject} from "rxjs";

@NgModule({
    imports: [HttpClientModule],
    providers: [ProductRepository, CategoryRepository,
        Filter, {provide: CATEGORY_ID, useValue: new Subject<number>()},    
        ProductRest, {provide: PRODUCT_URl, useValue: `https://${location.hostname}:5001/api/products`},
        CategoryRest, {provide: CATEGORY_URL, useValue: `https://${location.hostname}:5001/api/categories`}],
})
export class ModelModule { }