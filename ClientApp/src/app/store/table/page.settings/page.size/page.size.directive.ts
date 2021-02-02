import {Directive, ElementRef, EventEmitter, Inject, InjectionToken, Input, Output, SimpleChange} from "@angular/core";
import {BehaviorSubject, Subject} from "rxjs";

export const NEW_SIZE = new InjectionToken("new_size");

export const DISPLAY_EMITTER = new InjectionToken("display_emitter");

export const CURRENT_VALUE = new InjectionToken("current_value");

@Directive({
    selector: "[sp-page-size]",
    providers: [{ provide: NEW_SIZE, useValue: new BehaviorSubject<number>(null)},
        { provide: DISPLAY_EMITTER, useValue: new BehaviorSubject<string>(null)},
        { provide: CURRENT_VALUE, useValue: new BehaviorSubject<number>(null)}],
    exportAs: "SpDisplay"
})
export class SpPageSize {
    
    constructor(element: ElementRef, @Inject(NEW_SIZE) private sizeEvent: Subject<number>,
                @Inject(DISPLAY_EMITTER) private displayEmitter: Subject<string>,
                @Inject(CURRENT_VALUE) private valueEvent: Subject<number>) {
        this.sizeEvent.subscribe(size => {
            if (size) {
                if (size != this.currentValue){
                    valueEvent.next(size);
                    this.changeSize.emit(size);
                }
                
                if (element.nativeElement.classList.contains("_active")){
                    element.nativeElement.classList.toggle("_active");
                }
            }
        });
        displayEmitter.subscribe(str => {
            if (str) {
                this.displayVal = str;
                if (str == "block") {
                    if (!element.nativeElement.classList.contains("_active")){
                        element.nativeElement.classList.toggle("_active");
                    }
                }
                if (str == "none"){
                    if (element.nativeElement.classList.contains("_active")){
                        element.nativeElement.classList.toggle("_active");
                    }
                }
            } 
        });
        valueEvent.subscribe(val => {
            this.currentValue = val;
        });
    }
    
    currentValue?: number;
    
    @Input("sp-page-size")
    newSize?: number;
    
    @Output("sp-change-size")
    changeSize: EventEmitter<number> = new EventEmitter<number>();
    
    ngOnChanges(changes: {[property: string] : SimpleChange}) {
        let change = changes["newSize"];
        if (change) {
            this.valueEvent.next(change.currentValue);
            this.sizeEvent.next(change.currentValue);
        }
    }
    
    displayVal: string;
    
    display() {
        if (this.displayVal != "block") {
            this.displayEmitter.next("block");
        }
        else {
            this.displayEmitter.next("none");
        }
    }
}