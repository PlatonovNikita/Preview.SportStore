import {NgModule} from "@angular/core";
import {HttpClientModule} from "@angular/common/http";
import {ProductRepository} from "./product.repository";
import {CategoryRepository} from "./category.repository";

@NgModule({
    imports: [HttpClientModule],
    providers: [ProductRepository, CategoryRepository]
})
export class ModelModule { }