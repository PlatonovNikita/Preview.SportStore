import {Component} from "@angular/core";
import {ProductRepository} from "../../../model/product.repository";
import {Product} from "../../../model/product/product.model";
import {CategoryRepository} from "../../../model/category.repository";
import {Category} from "../../../model/category/category.model";
import {GroupProperty} from "../../../model/category/groupProperty.model";

@Component({
    selector: "spCatalogProducts",
    templateUrl: "catalog.products.component.html"
})
export class CatalogProductsComponent {
    products: Product[] = [];
    category: Category;
    globalProp: GroupProperty;
    
    constructor(private productsRepository: ProductRepository,
                private categoriesRepository: CategoryRepository) {
        productsRepository.newProducts.subscribe(() => {
            this.products = productsRepository.products;
        });
        categoriesRepository.newCategory.subscribe(() => {
            this.category = categoriesRepository.category;
            this.globalProp = categoriesRepository.category?.groupProperties
                ?.find(gp => gp.name == "_global");
        });
    }
    
    getPropVal(product: Product, propId: number, groupId: number) {
        let groupVal = product?.groupsValues
            ?.find(gv => gv.groupPropertyId == groupId);
        let doubleVal = groupVal?.doubleProps
            ?.find(bp => bp.propertyId == propId);
        if (doubleVal) {
            return doubleVal.value;
        }
        let boolVal = groupVal?.boolProps
            ?.find(bp => bp.propertyId == propId);
        if (boolVal) {
            if (boolVal.value) {
                return "Да";
            }
            else {
                return "Нет";
            }
        }
        let strVal = groupVal?.strProps
            ?.find(sp => sp.propertyId == propId);
        if (strVal) {
            return strVal.value;
        }
    }
}