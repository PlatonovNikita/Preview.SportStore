import {BoolLineSearch} from "../../../model/ViewModel/boolLineSearch.model";
import {Component, Inject} from "@angular/core";
import {Observable, Observer, Subject} from "rxjs";
import {CategoryRest} from 'src/app/model/category.rest';
import {Category, CATEGORY_ID} from 'src/app/model/category/category.model';
import {GroupProperty} from 'src/app/model/category/groupProperty.model';
import {UniqueString} from 'src/app/model/category/uniqueString';
import {BOOL_FILTER, BoolFilterState} from './bool.filter/bool.filter.state';
import {NUMBER_FILTER, NumberFilterState} from './number.filter/number.filter.state';
import {PRICE_FILTER, PriceFilterState} from './price.filter/price.filter.state';
import {Operation, STR_FILTER, StrFilterState} from './str.filter/str.filter.state';
import {ProductRepository} from "../../../model/product.repository";
import {Property, PropertyType} from "../../../model/category/property.model";
import {StrLineSearch} from "../../../model/ViewModel/strLineStarch";
import {DoubleLineSearch} from "../../../model/ViewModel/doubleLineSearch.model";
import {Filter} from "../../../model/product/configClasses.repository";
import {CLEAR_EMITTER} from "../../store.module";
import {SectionTrigger} from "../animations/table.animations";

@Component({
    selector: "spFilter",
    templateUrl: "filter.component.html",
    animations: [SectionTrigger]
})
export class FilterComponent {
    category: Category;
    uniqueStrings: { [propertyId: number]: UniqueString[]} = {};
    wait: boolean = false;
    public strEvent: Subject<StrFilterState> = new Subject<StrFilterState>();
    public numberEvent: Subject<NumberFilterState> = new Subject<NumberFilterState>();
    public boolEvent: Subject<BoolFilterState> = new Subject<BoolFilterState>();
    public priceEvent: Subject<PriceFilterState> = new Subject<PriceFilterState>();
    
    
    constructor(private rest: CategoryRest, private productModel: ProductRepository,
                private filter: Filter,
                @Inject(CATEGORY_ID) private id: Observable<number>,
                @Inject(CLEAR_EMITTER) public clearEmitter: Subject<void>) {
        id.subscribe(catId => rest.getCategory(catId)
            .subscribe(cat => {
                for (let gp of cat.groupProperties){
                    for (let prop of gp.properties){
                        if (prop.propType == PropertyType.Str){
                            this.rest.getUniqueString(prop.id).subscribe(us => {
                                this.uniqueStrings[prop.id] = us;
                            });
                        }
                    }
                }
                this.category = cat;
            }
        ));
        /*rest.getCategory(2).subscribe(cat => {
            for (let gp of cat.groupProperties){
                for (let prop of gp.properties){
                    if (prop.propType == PropertyType.Str){
                        this.rest.getUniqueString(prop.id).subscribe(us => {
                            this.uniqueStrings[prop.id] = us;
                        });
                    }
                }
            }
            this.category = cat;
        });*/

        this.priceEvent.subscribe(priceState => {
            if (priceState.isMax == true) {
                this.filter.maxPrice = priceState.value;
            }
            if (priceState.isMax == false) {
                this.filter.minPrice = priceState.value;
            }
        });

        this.boolEvent.subscribe(boolState => {
            let boolLineIndex = this.filter._searchLines.BSearch
                .findIndex(bs => bs.propertyId == boolState.propertyId);
            let boolLine = this.filter._searchLines.BSearch[boolLineIndex];
            if (boolLine){
                if (boolState.value == null){
                    this.filter._searchLines.BSearch.splice(boolLineIndex,1);
                }
                else {
                    boolLine.value = boolState.value;
                }
            }
            else {
                this.filter._searchLines.BSearch
                    .push(new BoolLineSearch(boolState.propertyId, boolState.value));
            }
        });

        this.strEvent.subscribe(strState => {
            let strLineIndex = this.filter._searchLines.StrSearch
                .findIndex(strS => strS.propertyId == strState.propertyId);
            let strLine = this.filter._searchLines.StrSearch[strLineIndex];

            if (strState.value){
                if (strLine){
                    let strIndex = strLine.strings
                        .findIndex(str => str == strState.value);
                    if (strState.operation == Operation.Add && strIndex == -1){
                        strLine.strings.push(strState.value);
                    }
                    if (strState.operation == Operation.Remove && strIndex != -1){
                        strLine.strings.splice(strIndex, 1);
                        if (strLine.strings.length == 0) this.filter._searchLines.StrSearch
                            .splice(strLineIndex, 1);
                    }
                }
                else if (strState.operation == Operation.Add){
                    this.filter._searchLines.StrSearch.push(new StrLineSearch(strState.propertyId, [strState.value]));
                }
            }

        });

        this.numberEvent.subscribe(numState => {
            let numLineIndex = this.filter._searchLines.DSearch
                .findIndex(ns => ns.PropertyId == numState.propertyId);
            let numLine = filter._searchLines.DSearch[numLineIndex];

            if (numLine) {
                if (numState.isMax && !numState.value && !numLine.min
                    || !numState.isMax && !numState.value && !numLine.max)
                {
                    this.filter._searchLines.DSearch.splice(numLineIndex, 1);
                }
                else {
                    if (numState.isMax) numLine.max = numState.value;
                    if (!numState.isMax) numLine.min = numState.value;
                }
            }
            else {
                if (numState.isMax){
                    this.filter._searchLines.DSearch
                        .push(new DoubleLineSearch(numState.propertyId, null, numState.value));
                }
                else {
                    this.filter._searchLines.DSearch
                        .push(new DoubleLineSearch(numState.propertyId, numState.value, null));
                }
            }
        });
    }
    
    getCategory(): Category{
        return this.category;
    }
    
    getGlobalProperty(): GroupProperty{
        return this.category?.groupProperties?.find(g => g.name == "_global");
    }
    
    getGroup(): GroupProperty[]{
        return this.category?.groupProperties?.filter(g => g.name != "_global");
    }
    
    getPropertiesFromGroup(group: GroupProperty): Property[]{
        return group?.properties.sort((a,b) => {
            if (a.propType < b.propType) return -1;
            if (a.propType == b.propType) return  0;
            if (a.propType > b.propType) return  1;
        });
    }
    
    submit(){
        this.productModel.getProducts();
    }
    
    reset(){
        this.filter.reset();
        this.clearEmitter.next();
    }

    getUniqueString(propertyId: number): UniqueString[]{
        return this.uniqueStrings[propertyId];
    }
    
    
}