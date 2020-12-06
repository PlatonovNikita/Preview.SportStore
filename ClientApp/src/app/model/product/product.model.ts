import {GroupValues} from "./groupValues.model";
import {Category} from "../category/category.model";

export class Product {
    constructor(
        public id?: number,
        public name?: string,
        public description?: string,
        public price?: number,
        public inStock?: boolean,
        public categoryId?: number,
        public category?: Category,
        public groupsValues?: GroupValues[]
    ) { }
}