import {Directive, Injectable, InjectionToken} from "@angular/core";
import {Subject} from "rxjs";

export const ANIMATE_STATE = new InjectionToken("animate_state");

export  const ANIMATE_EMITTER = new InjectionToken("animate_emitter");

@Injectable()
export class AniState {
    public display?: string;
    
    constructor() {
    }
}

@Directive({
    selector: "[sp-animate]",
    providers: [{ provide: ANIMATE_STATE, useClass: AniState},
        { provide: ANIMATE_EMITTER, useFactory: () => new Subject<string>() }]
})
export class SpAnimate {
    
}