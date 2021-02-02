import {Directive, HostBinding, HostListener, Inject, Input} from "@angular/core";
import {Observable, Observer, Subject} from "rxjs";
import {BoolFilterState} from "./bool.filter.state";
import {CLEAR_EMITTER} from "../filter.component";

@Directive({
    selector: "[sp-bool-filter]"
})
export class SpBoolFilterDirectory{
    
    constructor(@Inject(CLEAR_EMITTER) private clearEvent: Observable<void>) {
        this.clearEvent.subscribe(() => {
            this.fieldValue = false;
        });
    }
    
    @Input("sp-property-id")
    propertyId?: number;
    
    @Input("sp-bool-filter")
    boolState?: Subject<BoolFilterState>;
    
    @Input("sp-value")
    value?: boolean;

    @HostBinding("checked")
    fieldValue: boolean = false;
    
    ngOnInit(){
        let event = this.boolState as Observable<BoolFilterState>;
        event.subscribe(boolState => {
            if (boolState.propertyId == this.propertyId 
                && this.value != null
                && boolState.value == (!this.value))
            {
                this.fieldValue = false;
            }
        });
    }
    
    @HostListener("change", ["$event.target.checked"])
    updateValue(newValue: boolean){
        if (this.propertyId && this.value != null && this.boolState){
            if (newValue == true){
                this.boolState.next(new BoolFilterState(this.propertyId, this.value));
            }
            if (newValue == false){
                this.boolState.next(new BoolFilterState(this.propertyId, null));
            }
        }
        this.fieldValue = newValue;
    }
}