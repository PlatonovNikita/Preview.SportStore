import {BoolLineSearch} from "./boolLineSearch.model";
import {DoubleLineSearch} from "./doubleLineSearch.model";

export class SearchLines {
    
    constructor(
        public BSearch?: BoolLineSearch[],
        public DSearch?: DoubleLineSearch[]
    ) { }
}