import {SearchLines} from "../ViewModel/searchLines.model";
import {Inject, Injectable} from "@angular/core";

@Injectable( )
export class Filter {
    public pageSize?: number = 9;
    public pageNumber?: number = 1;
    public search?: string = null; 
    public categoryId?: number = null;
    public inStock?: boolean = null; 
    public minPrice?: number = null;
    public maxPrice?: number = null; 
    public _searchLines: SearchLines = new SearchLines();
    
    get searchLines(): SearchLines{
        if (this._searchLines.BSearch.length == 0 
            && this._searchLines.DSearch.length == 0 
            && this._searchLines.StrSearch.length == 0){
            return null;
        }
        return this._searchLines;
    }
    
    reset() {
        this.pageSize = 9;
        this.pageNumber = 1;
        this.search = null;
        this.inStock = null;
        this.minPrice = null;
        this.maxPrice = null;
        this._searchLines = new SearchLines();
    }
    
    
}