import {GroupValues} from "./groupValues.model";
import {Category} from "../category/category.model";
import {Description} from "./description.model";

export class Product {
    constructor(
        public id?: number,
        public name?: string,
        public description?: Description,
        public price?: number,
        public inStock?: boolean,
        public categoryId?: number,
        public category?: Category,
        public groupsValues?: GroupValues[]
    ) { }
}