import {Inject, Injectable, InjectionToken} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable, Subject} from "rxjs";
import {Category} from "./category/category.model";
import {GroupProperty} from "./category/groupProperty.model";
import {Property} from "./category/property.model";
import {UniqueString} from "./category/uniqueString";

export const CATEGORY_URL = new InjectionToken("category_url");

@Injectable()
export class CategoryRest{
    
    constructor(private http: HttpClient,
                @Inject(CATEGORY_URL) private categoryUrl: string) { }
    
    getCategory(id: number): Observable<Category>{
        return this.http.get<Category>(`${this.categoryUrl}/${id}`);
    }
    
    getCategoryByNickName(nickName: string): Observable<Category> {
        return this.http.get<Category>(`${this.categoryUrl}/byNick/${nickName}`);
    }

    getCategories(search?: string): Observable<Category[]>{
        let url = this.categoryUrl;
        if (search) url += `?search=${search}`;
        return this.http.get<Category[]>(url);
    }

    createCategory(category: Category): Observable<void> {
        let result = new Subject<void>();
        let data = {
            name: category.name
        };

        let categoryId = this.http.post<number>(this.categoryUrl, data);
        categoryId.subscribe(catId => {
            category.id = catId;
            category.groupProperties.forEach(gp => {
                gp.categoryId = catId;
                this.createGroup(gp);
            });
            result.next();
        });
        return result;
    }

    createGroup(group: GroupProperty): Observable<void> {
        if (group.categoryId != null) {
            let result = new Subject<void>();
            let data = {
                name: group.name,
                categoryId: group.categoryId
            };

            let groupId = this.http.post<number>(`${this.categoryUrl}/group`, data);
            groupId.subscribe(groupId => {
                group.id = groupId;
                group.properties.forEach(prop => {
                    prop.groupPropertyId = groupId;
                    this.createProperty(prop);
                });
                result.next();
            });
            return result;
        }
    }

    createProperty(property: Property): Observable<void> {
        if (property.groupPropertyId != null) {
            let result = new Subject<void>();
            let data = {
                name: property.name,
                propertyType: property.propType,
                groupPropertyId: property.groupPropertyId
            };

            this.http.post<number>(`${this.categoryUrl}/property`, data)
                .subscribe(propId => {
                    property.id = propId;
                    result.next();
                });
            
            return result;
        }
    }

    updateCategory(category: Category): Observable<unknown> {
        if (category.id != null) {
            return this.http.put(`${this.categoryUrl}/${category.id}`, category.name);
        }
    }

    updateGroup(group: GroupProperty): Observable<unknown>{
        if (group.id != null) {
            return this.http.put(`${this.categoryUrl}/group/${group.id}`, group.name);
        }
    }

    updateProperty(property: Property): Observable<unknown> {
        if (property.id != null) {
            let data = {
                name: property.name,
                propertyType: property.propType
            }
            return this.http.put(`${this.categoryUrl}/property/${property.id}`, data);
        }
    }

    deleteCategory(id: number): Observable<unknown> {
        return this.http.delete(`${this.categoryUrl}/${id}`);
    }

    deleteGroup(id: number): Observable<unknown> {
        return this.http.delete(`${this.categoryUrl}/group/${id}`);
    }

    deleteProperty(id: number): Observable<unknown> {
        return this.http.delete(`${this.categoryUrl}/property/${id}`);
    }
    
    getUniqueString(propertyId: number): Observable<UniqueString[]> {
        return this.http.get<UniqueString[]>(`${this.categoryUrl}/uniquestrings/${propertyId}`);
    }
}