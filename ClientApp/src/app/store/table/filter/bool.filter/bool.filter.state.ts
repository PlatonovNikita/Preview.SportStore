import {InjectionToken} from "@angular/core";

export const BOOL_FILTER = new InjectionToken("bool_filter");

export class BoolFilterState{
    
    constructor(public propertyId: number,
                public value?: boolean) {
    }
}