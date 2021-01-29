import {InjectionToken} from "@angular/core";

export const STR_FILTER = new InjectionToken("str_filter");

export enum Operation{
    Add,
    Remove
}

export class StrFilterState{
    
    constructor(public value: string,
                public propertyId: number,
                public operation: Operation) {
    }
}