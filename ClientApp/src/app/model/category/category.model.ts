import {GroupProperty} from "./groupProperty.model";
import {Product} from "../product/product.model";

export class Category {
    constructor(
        public id?: number,
        public name?: string,
        public nikName?: string,
        public groupProperties?: GroupProperty[],
        public products?: Product[]
    ) { }
}