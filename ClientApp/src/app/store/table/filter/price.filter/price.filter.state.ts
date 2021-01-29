import {InjectionToken} from "@angular/core";

export const PRICE_FILTER = new InjectionToken("price_filter");

export class PriceFilterState {
    constructor(public value?: number,
        public isMax?: boolean) {
    }
} 