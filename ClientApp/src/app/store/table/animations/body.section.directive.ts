import {Directive, ElementRef, EventEmitter, HostListener, Inject, Input, Output} from "@angular/core";
import {ANIMATE_EMITTER, ANIMATE_STATE, AniState} from "./animate.directive";
import {Observable, Observer} from "rxjs";
import {debounceTime} from "rxjs/operators";

@Directive({
    selector: "[sp-body-section]",
    exportAs: "spBody"
})
export class SpBodySection {
    constructor(@Inject(ANIMATE_STATE) private state: AniState,
                @Inject(ANIMATE_EMITTER) private aniEvent: Observable<string>) {
        aniEvent.subscribe(str => {
            this.currentValue = str;
        })
    }
    
    currentValue : string;
    
    done(){
        this.state.display = this.currentValue;
    }
}