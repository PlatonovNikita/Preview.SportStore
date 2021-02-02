import {Directive, HostBinding, HostListener, Inject, Input} from "@angular/core";
import {Observable, Observer} from "rxjs";
import {PriceFilterState} from "./price.filter.state";
import {CLEAR_EMITTER} from "../filter.component";
@Directive({
    selector: "[sp-price-filter]"
})
export class SpPriceFilter {

    constructor(@Inject(CLEAR_EMITTER) private clearEvent: Observable<void>) {
        this.clearEvent.subscribe(() => {
            this.fieldValue = "";
        });
    }
    
    @Input("sp-price-filter")
    priceEventEmitter?: Observer<PriceFilterState>;
    
    @Input("sp-is-max")
    isMax?: boolean;
    
    @HostBinding("value")
    fieldValue: string = "";
    
    @HostListener("change", ["$event.target.value"])
    updateValue(newValue: string){
        if (this.priceEventEmitter && this.isMax != null){
            let newPrice = Number(newValue);
            if (isNaN(newPrice)) newPrice = null;
            this.priceEventEmitter.next(new PriceFilterState(newPrice, 
                this.isMax)); 
        }
        this.fieldValue = newValue;
    }
}