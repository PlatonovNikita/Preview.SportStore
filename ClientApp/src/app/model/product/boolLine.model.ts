import {Property} from "../category/property.model";

export class BoolLine {
    
    constructor(
        public id?: number,
        public value?: boolean,
        public propertyId?: number,
        public property?: Property,
        public groupValuesId?: number
    ) { }
}