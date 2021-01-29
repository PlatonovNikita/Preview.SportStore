import {Directive, HostBinding, HostListener, Inject, Input} from "@angular/core";
import {Observable, Observer} from "rxjs";
import {NumberFilterState} from "./number.filter.state";
import {CLEAR_EMITTER} from "../../../store.module";

@Directive({
    selector: "[sp-number-filter]"
})
export class SpNumberFilterDirective{

    constructor(@Inject(CLEAR_EMITTER) private clearEvent: Observable<void>) {
        this.clearEvent.subscribe(() => {
            this.fieldValue = "";
        });
    }
    
    @Input("sp-number-filter")
    numberEventEmitter?: Observer<NumberFilterState>;
    
    @Input("sp-property-id")
    propertyId?: number;
    
    @Input("sp-is-max")
    isMax?: boolean;
    
    @HostBinding("value")
    fieldValue: string = "";
    
    @HostListener("change", ["$event.target.value"])
    updateValue(newValue: string){
        let newNumber = Number(newValue);
        if (!newValue) newNumber = null;
        if (this.numberEventEmitter && this.propertyId 
            && this.isMax != null)
        {
            if (isNaN(newNumber)) newNumber = null;
            this.numberEventEmitter
                .next(new NumberFilterState(this.isMax, 
                    this.propertyId, newNumber));    
        }
        this.fieldValue = newValue;
    }
}