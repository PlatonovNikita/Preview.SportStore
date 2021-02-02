import {Injectable} from "@angular/core";
import {Product} from "./product/product.model";
import {GroupValues} from "./product/groupValues.model";
import {BoolLine} from "./product/boolLine.model";
import {DoubleLine} from "./product/doubleLine.model";
import {StrLine} from "./product/strLine.model";
import {Filter} from "./product/configClasses.repository";
import {ProductRest} from "./product.rest";
import {BehaviorSubject, Subject} from "rxjs";

@Injectable()
export class ProductRepository {
    product: Product = new Product();
    products: Product[] = [];
    productId?: number;
    newProducts: Subject<void> = new Subject<void>();

    constructor(private rest: ProductRest, private filter: Filter) {
    }

    getProduct(id: number) {
        this.productId = id;
        this.rest.getProduct(id)
            .subscribe(p => this.product = p);
    }

    getProducts() {
        console.log(this.filter);
        this.rest.getProducts(this.filter)
            .subscribe(p => {
                this.products = p;
                this.newProducts.next();
            });
    }
    
    createProduct(product: Product){
        if (product.categoryId != null) {
            this.rest.createProduct(product)
                .subscribe(prodId => {
                    this.products.push(product); 
                });
        }
    }
    
    CreateProperty(groupValue: GroupValues, value: any){
        if (groupValue.productId != null && groupValue.groupPropertyId != null){
            switch (typeof value){
                case typeof BoolLine: 
                    this.rest.createBoolProperty(groupValue.productId, groupValue.groupPropertyId, value)
                        .subscribe(() => groupValue.boolProps.push(value)); 
                    break;
                case typeof DoubleLine:
                    this.rest.createDoubleProperty(groupValue.productId, groupValue.groupPropertyId, value)
                        .subscribe(() => groupValue.doubleProps.push(value));
                    break;
                case typeof StrLine:
                    this.rest.createStringProperty(groupValue.productId, groupValue.groupPropertyId, value)
                        .subscribe(() => groupValue.strProps.push(value));
                    break;
            }
        }
    }

    replaceProduct(product: Product) {
        this.rest.replaceProduct(product)
            .subscribe(() => this.getProducts());
    }
    
    updateProduct(id: number, changes: Map<string, any>) {
        this.rest.updateProduct(id, changes)
            .subscribe(() => this.getProducts());
    }
    
    deleteProduct(id: number) {
        this.rest.deleteProduct(id)
            .subscribe(() => this.getProducts());
    }
}