import {Directive, HostBinding, HostListener, Inject, Input} from "@angular/core";
import {Observable, Observer} from "rxjs";
import {Operation, StrFilterState} from "./str.filter.state";
import {CLEAR_EMITTER} from "../../../store.module";

@Directive({
    selector: "[sp-str-filter]"
})
export class SpStrFilterDirectory{

    constructor(@Inject(CLEAR_EMITTER) private clearEvent: Observable<void>) {
        this.clearEvent.subscribe(() => {
            this.fieldValue = false;
        });
    }
    
    previousValue?: boolean = null;
    
    @Input("sp-property-id")
    propertyId: number;
    
    @Input("sp-value")
    value: string;
    
    @Input("sp-str-filter")
    strEventEmitter: Observer<StrFilterState>;
    
    @HostBinding("checked")
    fieldValue: boolean;
    
    @HostListener("change", ["$event.target.checked"])
    UpdateValue(newValue: boolean){
        if (this.propertyId && this.value && this.strEventEmitter){
            if (newValue == true && this.fieldValue != true){
                this.strEventEmitter.next(new StrFilterState(this.value, this.propertyId, Operation.Add));
            }
            if (newValue == false && this.fieldValue != false){
                this.strEventEmitter.next(new StrFilterState(this.value, this.propertyId, Operation.Remove));
            }
        }
        this.fieldValue = newValue;
    }
}