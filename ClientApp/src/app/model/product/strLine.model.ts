import {Property} from "../category/property.model";

export class StrLine {
    
    constructor(
        public id?: number,
        public value?: string,
        public propertyId?: number,
        public property?: Property,
        public groupValuesId?: number
    ) { }
}