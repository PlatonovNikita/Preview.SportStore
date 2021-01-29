import {Directive, ElementRef, HostListener, Inject} from "@angular/core";
import {ANIMATE_EMITTER, ANIMATE_STATE, AniState} from "./animate.directive";
import {Observer} from "rxjs";

@Directive({
    selector: "[sp-title-section]"
})
export class SpTitleSection {
    
    constructor(@Inject(ANIMATE_STATE) private state: AniState,
                @Inject(ANIMATE_EMITTER) private aniEmitter: Observer<string>,
                private element: ElementRef) {
    }
    
    previousTrigger: string;
    
    @HostListener("click")
    EmitAnimation() {
        if (this.previousTrigger == null) {
            if (this.element.nativeElement.classList.contains("_active")) {
                this.aniEmitter.next("none");
                this.previousTrigger = "none";
            } else {
                this.aniEmitter.next("block");
                this.previousTrigger = "block";
            }
            this.element.nativeElement.classList.toggle("_active");
        } else if (this.previousTrigger == this.state.display) {
            if (this.previousTrigger == "block") {
                this.aniEmitter.next("none");
                this.previousTrigger = "none";
            } else {
                this.aniEmitter.next("block");
                this.previousTrigger = "block";
            }
            this.element.nativeElement.classList.toggle("_active");
        }
    }
}