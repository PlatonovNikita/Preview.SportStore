import {DoubleLine} from "./doubleLine.model";
import {BoolLine} from "./boolLine.model";
import {StrLine} from "./strLine.model";
import {GroupProperty} from "../category/groupProperty.model";

export class GroupValues {
    
    constructor(
        public id?: number,
        public groupPropertyId?: number,
        public groupProperty?: GroupProperty,
        public productId?: number,
        public doubleProps?: DoubleLine[],
        public boolProps?: BoolLine[],
        public strProps?: StrLine[]
    ) { }
}