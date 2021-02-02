import {Directive, ElementRef, HostListener, Inject, Input} from "@angular/core";
import {Subject} from "rxjs";
import {DISPLAY_EMITTER, NEW_SIZE} from "./page.size.directive";

@Directive({
    selector: "[sp-option-size]"
})
export class SpOptionSize {

    constructor(element: ElementRef, @Inject(NEW_SIZE) private sizeEvent: Subject<number>,
                @Inject(DISPLAY_EMITTER) private displayEmitter: Subject<string>) {
        this.sizeEvent.subscribe(size => {
            if (size) {
                if (size == this.value) {
                    element.nativeElement.style["display"] = "none";
                }
                else {
                    element.nativeElement.style["display"] = "block";
                }
            }
        });
    }
    
    @Input("sp-option-size")
    value: number;
    
    @HostListener("click")
    updateSize() {
        this.sizeEvent.next(this.value);
        this.displayEmitter.next("none");
    }
}