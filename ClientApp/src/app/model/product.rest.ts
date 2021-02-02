import {Inject, Injectable, InjectionToken} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable, Subject} from "rxjs";
import {Product} from "./product/product.model";
import {Filter} from "./product/configClasses.repository";
import {GroupValues} from "./product/groupValues.model";
import {BoolLine} from "./product/boolLine.model";
import {DoubleLine} from "./product/doubleLine.model";
import {StrLine} from "./product/strLine.model";

export const PRODUCT_URl = new InjectionToken("product_url");

@Injectable()
export class ProductRest {
    
    constructor(private http: HttpClient, 
        @Inject(PRODUCT_URl) private productsUrl: string ) { }
    
    getProduct(id: number): Observable<Product>{
        return this.http.get<Product>(`${this.productsUrl}/${id}`);
    }
    
    getPagesCount(filter: Filter): Observable<number> {
        let url = this.productsUrl;
        
        url += `/pagesCount?`;

        if (filter?.pageSize) url += `pageSize=${filter.pageSize}&`;

        if (filter?.search) url += `search=${filter.search}&`;

        if (filter?.categoryId) url += `categoryId=${filter.categoryId}&`;

        if (filter?.inStock) url += `inStock=${filter.inStock}&`;

        if (filter?.minPrice) url += `minPrice=${filter.minPrice}&`;

        if (filter?.maxPrice) url += `maxPrice=${filter.maxPrice}&`;
        
        if (filter?.searchLines){
            return this.http.post<number>(url, filter.searchLines);
        }
        else {
            return this.http.get<number>(url);
        }
        
    }
    
    getProducts(filter: Filter): Observable<Product[]>{
        let url = this.productsUrl;

        if (filter?.searchLines) {
            url += `/filter?`;
        } else url += `?`;

        if (filter?.pageSize) url += `pageSize=${filter.pageSize}&`;

        if (filter?.pageNumber) url += `pageNumber=${filter.pageNumber}&`;

        if (filter?.search) url += `search=${filter.search}&`;

        if (filter?.categoryId) url += `categoryId=${filter.categoryId}&`;

        if (filter?.inStock) url += `inStock=${filter.inStock}&`;

        if (filter?.minPrice) url += `minPrice=${filter.minPrice}&`;

        if (filter?.maxPrice) url += `maxPrice=${filter.maxPrice}&`;

        if (filter?.searchLines) {
            return this.http.post<Product[]>(url, filter.searchLines)
        } else {
            return this.http.get<Product[]>(url)
        }
    }
    
    createProduct(product: Product): Observable<unknown> {
        if (product.categoryId != null) {
            let result = new Subject();
            let data = {
                name: product.name,
                description: product.description,
                price: product.price,
                inStock: product.inStock,
                categoryId: product.categoryId
            };

            let productId = this.http.post<number>(this.productsUrl, data)
                .subscribe(prodId => {
                    product.id = prodId;
                    product.groupsValues.forEach(gv => {
                        gv.productId = prodId;
                        this.createProperties(gv);
                    });
                    result.next();
                });
            
            return result;
        }
    }

    createProperties(groupValues: GroupValues) {
        if (groupValues.productId != null && groupValues.groupPropertyId != null) {
            groupValues.boolProps.forEach(b => {
                this.createBoolProperty(groupValues.productId, groupValues.groupPropertyId, b);
            });
            groupValues.doubleProps.forEach(d => {
                this.createDoubleProperty(groupValues.productId, groupValues.groupPropertyId, d);
            });
            groupValues.strProps.forEach(s => {
                this.createStringProperty(groupValues.productId, groupValues.groupPropertyId, s);
            });
        }
    }

    createBoolProperty(productId: number, groupPropertyId: number, boolProp: BoolLine): Observable<unknown> {
        if (boolProp.propertyId != null) {
            let result = new Subject();
            let data = {
                productId: productId,
                groupPropertyId: groupPropertyId,
                propertyId: boolProp.propertyId,
                value: boolProp.value
            }

            this.http.post<number>(`${this.productsUrl}/bproperty`, data)
                .subscribe(bPropId => {
                    boolProp.id = bPropId;
                    result.next();
                });
            
            return result;
        }
    }

    createDoubleProperty(productId: number, groupPropertyId: number, doubleProp: DoubleLine): Observable<unknown> {
        if (doubleProp.propertyId != null) {
            let result = new Subject();
            let data = {
                productId: productId,
                groupPropertyId: groupPropertyId,
                propertyId: doubleProp.propertyId,
                value: doubleProp.value
            }

            this.http.post<number>(`${this.productsUrl}/dproperty`, data)
                .subscribe(dPropId => {
                    doubleProp.id = dPropId;
                    result.next();
                });
            
            return result;
        }
    }

    createStringProperty(productId: number, groupPropertyId: number, strLine: StrLine): Observable<unknown> {
        if (strLine.propertyId != null) {
            let result = new Subject();
            let data = {
                productId: productId,
                groupPropertyId: groupPropertyId,
                propertyId: strLine.propertyId,
                value: strLine.value
            }

            this.http.post<number>(`${this.productsUrl}/sproperty`, data)
                .subscribe(sPropId => {
                    strLine.id = sPropId;
                    result.next();
                });
            
            return result;
        }
    }

    replaceProduct(product: Product): Observable<object> {
        if (product.id != null) {
            let data = {
                name: product.name,
                description: product.description,
                price: product.price,
                inStock: product.inStock
            }

            return this.http.put(`${this.productsUrl}/${product.id}`, data);
        }
    }

    updateProduct(id: number, changes: Map<string, any>) {
        let patch = [];
        changes.forEach((value, key) => {
            patch.push({op: "replace", path: key, value: value});
        });
        return this.http.patch(`${this.productsUrl}/${id}`, patch);
    }

    deleteProduct(id: number) {
        return this.http.delete(`${this.productsUrl}/${id}`);
    }

    deleteByGroup(groupId: number) {
        return this.http.delete(`${this.productsUrl}/bygroup/${groupId}`);
    }

    deleteByCategory(categoryId: number) {
        return this.http.delete(`${this.productsUrl}/bycategory/${categoryId}`);
    }

    deleteValue(propertyId: number) {
        return this.http.delete(`${this.productsUrl}/byproperty/${propertyId}`);
    }
}