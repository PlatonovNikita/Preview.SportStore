import {Component, Inject, InjectionToken} from "@angular/core";
import {Filter} from "../../../model/product/configClasses.repository";
import {ProductRepository} from "../../../model/product.repository";
import {ProductRest} from "../../../model/product.rest";
import {count, debounceTime, timeout} from "rxjs/operators";
import {CLEAR_EMITTER, SUBMIT_EMITTER} from "../filter/filter.component";
import {BehaviorSubject, Observable, Subject} from "rxjs";
import {CATEGORY_ID} from "../../../model/category/category.model";

export const PAGE_NUMBER = new InjectionToken("page_number");

export const PAGE_SIZE = new InjectionToken("page_size");

export const PAGE_COUNT = new InjectionToken("page_count");

@Component({
    selector: "spPageSettings",
    templateUrl: "page.settings.component.html",
    providers: [{ provide: PAGE_SIZE, useValue: new BehaviorSubject<number>(9)},
        { provide: PAGE_COUNT, useValue: new BehaviorSubject<number>(7)}]
})
export class PageSettingsComponent {
    pagesCount?: number = 7;
    pageSize: number = 9;
    numberSet: number[] = [];
    afterHalf: boolean = false;
    currentPage: number;
    
    constructor(private filter: Filter, private prodRepository: ProductRepository,
                private prodREST: ProductRest,
                @Inject(SUBMIT_EMITTER) private submitEmitter: Subject<void>,
                @Inject(PAGE_NUMBER) private pageNumber: BehaviorSubject<number>,
                @Inject(CATEGORY_ID) private id: Observable<number>,
                @Inject(CLEAR_EMITTER) private clearEmitter: Subject<void>,
                @Inject(PAGE_SIZE) private sizeEvent: BehaviorSubject<number>,
                @Inject(PAGE_COUNT) private countEvent: BehaviorSubject<number>) 
    {
        this.countEvent.subscribe(num => this.pagesCount = num);
        this.sizeEvent.subscribe(num => this.pageSize = num);
        this.pageNumber.subscribe(num => {
           this.updateNumberSet(num, this.pagesCount);
           this.currentPage = num;
        });
        id.subscribe(id => {
            if (id) {
                this.updateCountById(id);
            }
        });
        submitEmitter.subscribe(() => {
            this.updateCount(filter);
        });
        clearEmitter.subscribe(() => {
            this.updateCount(filter);
            filter.pageSize = this.pageSize;
        });
    }
    
    updateSize(size: number) {
        console.log(size);
        this.sizeEvent.next(size);
        this.filter.pageSize = size;
        this.updateCount(this.filter);
        this.prodRepository.getProducts();
    }
    
    updateCountById(categoryId: number) {
        let f = new Filter();
        f.categoryId = categoryId;
        this.updateCount(f);
    }
    
    updateCount(filter: Filter) {
        this.prodREST.getPagesCount(filter).subscribe(count => {
            this.countEvent.next(count);
            this.pageNumber.next(1);
        });
    }
    
    updateNumberSet(current: number, end: number) {
        let newSet = [];
        if (end > 7) {
            if (current > (end - 3)) {
                for (var i = end - 4; i <= end; i++) {
                    newSet.push(i);
                    this.afterHalf = true;
                }
            }
            else if (current > 3) {
                for (var i = current - 2; i <= current + 2; i++) {
                    newSet.push(i);
                    this.afterHalf = false;
                }
            }
            else {
                for (var i = 1; i <= 5; i++) {
                    newSet.push(i);
                    this.afterHalf = false;
                }
            }
        }
        else {
            for (var i = 1; i <= end; i++) {
                newSet.push(i);
                this.afterHalf = false;
            }
        }
        this.numberSet = newSet;
    }
    
    previousPage() {
        if (this.currentPage != 1 && this.currentPage != null) {
            this.filter.pageNumber = this.currentPage - 1;
            this.prodRepository.getProducts();
            this.pageNumber.next(this.currentPage - 1);
        }
    }
    
    nextPage() {
        if (this.currentPage != this.pagesCount && this.currentPage != null) {
            this.filter.pageNumber = this.currentPage + 1;
            this.prodRepository.getProducts();
            this.pageNumber.next(this.currentPage + 1);
        }
    }
}
