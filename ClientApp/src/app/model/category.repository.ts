import {Category} from "./category/category.model";
import {GroupProperty} from "./category/groupProperty.model";
import {Property} from "./category/property.model";
import {inject, Injectable} from "@angular/core";
import {CATEGORY_URL, CategoryRest} from "./category.rest";
import {MessageService} from "./message/message.service";
import {ProductRest} from "./product.rest";

const categoryUrl = "/api/categories";

@Injectable()
export class CategoryRepository {

    categories: Category[] = [];
    category: Category = new Category();
    search: string;

    constructor(private rest: CategoryRest,
                private productRest: ProductRest) { 
        this.getCategories();
    }

    getCategory(id: number) {
        this.rest.getCategory(id)
            .subscribe(c => this.category = c);
    }

    getCategories(){
        this.rest.getCategories(this.search)
            .subscribe(c => this.categories = c);
    }
    
    createCategory(category: Category) {
        this.rest.createCategory(category)
            .subscribe(() => this.categories.push(category));
    }
    
    createGroup(group: GroupProperty, category: Category) {
        this.rest.createGroup(group)
            .subscribe(() => category.groupProperties.push(group));
    }

    createProperty(property: Property, group: GroupProperty) {
        this.rest.createProperty(property)
            .subscribe(() => group.properties.push(property));
    }
    
    updateCategory(category: Category){
        this.rest.updateCategory(category)
            .subscribe(() => this.getCategories());
    }
    
    updateGroup(group: GroupProperty){
        this.rest.updateGroup(group)
            .subscribe(() => this.getCategories());
    }
    
    updateProperty(property: Property){
        this.rest.updateProperty(property)
            .subscribe(() => this.getCategories());
    }
    
    deleteCategory(id: number) {
        this.rest.deleteCategory(id)
            .subscribe(() => this.getCategories());
    }
    
    deleteGroup(groupId: number) {
        this.rest.deleteGroup(groupId)
            .subscribe(() => this.getCategories());
    }
    
    deleteProperty(propertyId: number) {
        this.rest.deleteProperty(propertyId)
            .subscribe(() => this.getCategories());
    }
}