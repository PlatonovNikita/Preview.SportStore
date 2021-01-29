import {animate, state, style, transition, trigger} from "@angular/animations";

export const SectionTrigger = trigger("sectionDisplay", [
    state("none", style({
        display: "none"
    })),
    state("block", style({
        display: "block"
    })),
    transition("* => none", animate("100ms")),
    transition("* => block", animate("100ms"))
]);