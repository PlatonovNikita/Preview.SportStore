import {SearchLines} from "../ViewModel/searchLines.model";

export class Filter {
    public pageSize?: number = 9;
    public pageNumber?: number = 1;
    public search?: string = null; 
    public categoryId?: number = null;
    public inStock?: boolean = null; 
    public minPrice?: number = null;
    public maxPrice?: number = null; 
    public searchLines?: SearchLines = null;
    
    reset() {
        this.pageSize = 9;
        this.pageNumber = 1;
        this.search = null;
        this.inStock = null;
        this.minPrice = null;
        this.maxPrice = null;
        this.searchLines = null
    }
}