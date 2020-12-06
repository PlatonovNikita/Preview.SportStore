import {Property} from "../category/property.model";

export class DoubleLine {
    
    constructor(
        public id?: number,
        public value?: number,
        public propertyId?: number,
        public property?: Property,
        public groupValuesId?: number
    ) { }
}