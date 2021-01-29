import {InjectionToken} from "@angular/core";

export const NUMBER_FILTER = new InjectionToken("number_filter");

export class NumberFilterState{
    constructor(public isMax: boolean,
                public propertyId: number,
                public value?: number) {
    }
}