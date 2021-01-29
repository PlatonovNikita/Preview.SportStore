import {GroupProperty} from "./groupProperty.model";
import {Product} from "../product/product.model";
import {InjectionToken} from "@angular/core";

export const CATEGORY_ID = new InjectionToken("category_id");

export class Category {
    constructor(
        public id?: number,
        public name?: string,
        public nikName?: string,
        public groupProperties?: GroupProperty[],
        public products?: Product[]
    ) { }
}