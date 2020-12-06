export enum PropertyType {
    Bool,
    Double,
    Str
}

export class Property {
    
    constructor(
        public id?: number,
        public name?: string,
        public propType?: PropertyType,
        public groupPropertyId?: number
    ) { }
}