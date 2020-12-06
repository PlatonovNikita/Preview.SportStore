import {Category} from "./category/category.model";
import {HttpClient} from "@angular/common/http";
import {GroupProperty} from "./category/groupProperty.model";
import {Property} from "./category/property.model";
import {Injectable} from "@angular/core";

const categoryUrl = "/api/categories";

@Injectable()
export class CategoryRepository {

    categories: Category[] = [];
    category: Category = new Category();

    constructor(private http: HttpClient) { 
        this.getCategories();
    }

    getCategory(id: number) {
        this.http.get<Category>(`${categoryUrl}/${id}`)
            .subscribe(c => this.category = c);
    }

    getCategories(){
        this.http.get<Category[]>(`${categoryUrl}`)
            .subscribe(c => this.categories = c);
    }
    
    createCategory(category: Category) {
        let data = {
            name: category.name
        };
        
        this.http.post<number>(categoryUrl, data)
            .subscribe(catId => {
                category.id = catId;
                category.groupProperties.forEach(gp => {
                    gp.categoryId = catId;
                    this.createGroup(gp);
                });
            }
        );
    }
    
    createGroup(group: GroupProperty) {
        if (group.categoryId != null) {
            let data = {
                name: group.name,
                categoryId: group.categoryId
            };
            
            this.http.post<number>(`${categoryUrl}/group`, data)
                .subscribe(groupId => {
                    group.id = groupId;
                    group.properties.forEach(prop => {
                        prop.groupPropertyId = groupId;
                        this.createProperty(prop);
                    });
                }
            );
        }
    }

    createProperty(property: Property) {
        if (property.groupPropertyId != null) {
            let data = {
                name: property.name,
                propertyType: property.propType,
                groupPropertyId: property.groupPropertyId
            };
            
            this.http.post<number>(`${categoryUrl}/property`, data)
                .subscribe(propId => property.id = propId);
        }
    }
    
    updateCategory(category: Category){
        if (category.id != null) {
            this.http.put(`${categoryUrl}/${category.id}`, category.name);
        }
    }
    
    updateGroup(group: GroupProperty){
        if (group.id != null) {
            this.http.put(`${categoryUrl}/group/${group.id}`, group.name);
        }
    }
    
    updateProperty(property: Property){
        if (property.id != null) {
            let data = {
                name: property.name,
                propertyType: property.propType
            }
            this.http.put(`${categoryUrl}/property/${property.id}`, data);
        }
    }
    
    deleteCategory(id: number) {
        this.http.delete(`${categoryUrl}/${id}`);
    }
    
    deleteGroup(id: number) {
        this.http.delete(`${categoryUrl}/group/${id}`);
    }
    
    deleteProperty(id: number) {
        this.http.delete(`${categoryUrl}/property/${id}`);
    }
}