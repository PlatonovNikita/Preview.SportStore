import {Property} from "./property.model";

export class GroupProperty {
    
    constructor(
        public id?: number,
        public name?: string,
        public categoryId?: number,
        public properties?: Property[]
    ) { }
}