import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Product} from "./product/product.model";
import {SearchLines} from "./ViewModel/searchLines.model";
import {Category} from "./category/category.model";
import {GroupValues} from "./product/groupValues.model";
import {BoolLine} from "./product/boolLine.model";
import {DoubleLine} from "./product/doubleLine.model";
import {StrLine} from "./product/strLine.model";
import {Filter} from "./product/configClasses.repository";

const productsUrl = "/api/products";

@Injectable()
export class ProductRepository {
    product: Product = new Product();
    products: Product[] = [];
    filter: Filter = new Filter();
    productId?: number;

    constructor(private http: HttpClient) {
        this.getProducts();
    }

    getProduct(id: number) {
        this.productId = id;
        this.http.get<Product>(`${productsUrl}/${id}`)
            .subscribe(p => this.product = p);
    }

    getProducts() {
        let url = productsUrl;

        if (this.filter.searchLines) {
            url += `/filter?`;
        } else url += `?`;

        if (this.filter.pageSize) url += `pageSize=${this.filter.pageSize}&`;

        if (this.filter.pageNumber) url += `pageNumber=${this.filter.pageNumber}&`;

        if (this.filter.search) url += `search=${this.filter.search}&`;

        if (this.filter.categoryId) url += `categoryId=${this.filter.categoryId}&`;

        if (this.filter.inStock) url += `inStock=${this.filter.inStock}&`;

        if (this.filter.minPrice) url += `minPrice=${this.filter.minPrice}&`;

        if (this.filter.maxPrice) url += `maxPrice=${this.filter.maxPrice}&`;

        if (this.filter.searchLines) {
            this.http.post<Product[]>(url, this.filter.searchLines)
                .subscribe(p => this.products = p);
        } else {
            this.http.get<Product[]>(url)
                .subscribe(p => this.products = p);
        }
    }
    
    createProduct(product: Product){
        if (product.categoryId != null) {
            let data = {
                name: product.name,
                description: product.description,
                price: product.price,
                inStock: product.inStock,
                categoryId: product.categoryId
            };
            
            this.http.post<number>(productsUrl, data)
                .subscribe(prodId => {
                    product.id = prodId;
                    product.groupsValues.forEach(gv => {
                        gv.productId = prodId;
                        this.createProperties(gv);
                    });
                   this.products.push(product); 
                });
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

    createBoolProperty(productId: number, groupPropertyId: number, boolProp: BoolLine) {
        if (boolProp.propertyId != null) {
            let data = {
                productId: productId,
                groupPropertyId: groupPropertyId,
                propertyId: boolProp.propertyId,
                value: boolProp.value
            }
            
            this.http.post<number>(`${productsUrl}/bproperty`, data)
                .subscribe(bPropId => boolProp.id = bPropId);
        }
    }


    createDoubleProperty(productId: number, groupPropertyId: number, doubleProp: DoubleLine) {
        if (doubleProp.propertyId != null) {
            let data = {
                productId: productId,
                groupPropertyId: groupPropertyId,
                propertyId: doubleProp.propertyId,
                value: doubleProp.value
            }

            this.http.post<number>(`${productsUrl}/dproperty`, data)
                .subscribe(dPropId => doubleProp.id = dPropId);
        }
    }

    createStringProperty(productId: number, groupPropertyId: number, strLine: StrLine) {
        if (strLine.propertyId != null) {
            let data = {
                productId: productId,
                groupPropertyId: groupPropertyId,
                propertyId: strLine.propertyId,
                value: strLine.value
            }

            this.http.post<number>(`${productsUrl}/sproperty`, data)
                .subscribe(sPropId => strLine.id = sPropId);
        }    
    }
    
    replaceProduct(product: Product) {
        if (product.id != null) {
            let data = {
                name: product.name,
                description: product.description,
                price: product.price,
                inStock: product.inStock
            }
            
            this.http.put(`${productsUrl}/${product.id}`, data)
                .subscribe(() => this.getProducts());
        }
    }
    
    updateProduct(id: number, changes: Map<string, any>) {
        let patch = [];
        changes.forEach((value, key) => {
            patch.push({op: "replace", path: key, value: value});
        });
        this.http.patch(`${productsUrl}/${id}`, patch)
            .subscribe(() => this.getProducts());
    }
    
    deleteProduct(id: number) {
        this.http.delete(`${productsUrl}/${id}`)
            .subscribe(() => this.getProducts());
    }
    
    deleteByGroup(id: number) {
        this.http.delete(`${productsUrl}/bygroup/${id}`);
    }
    
    deleteByCategory(id: number) {
        this.http.delete(`${productsUrl}/bycategory/${id}`);
    }
    
    deleteCategory(id: number) {
        this.http.delete(`${productsUrl}/byproperty/${id}`)
            .subscribe(() => this.getProduct(this.productId));
    }
}