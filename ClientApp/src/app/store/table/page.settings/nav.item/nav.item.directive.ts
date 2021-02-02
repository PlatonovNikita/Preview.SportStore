import {Directive, ElementRef, HostListener, Inject, Input} from "@angular/core";
import {PAGE_NUMBER} from "../page.settings.component";
import {BehaviorSubject, Subject} from "rxjs";
import {ProductRepository} from "../../../../model/product.repository";
import {Filter} from "../../../../model/product/configClasses.repository";

@Directive({
    selector: "[sp-nav-item]"
})
export class SpNavItem {
    
    constructor(@Inject(PAGE_NUMBER) private pageNumber: BehaviorSubject<number>,
                private products: ProductRepository, private filter: Filter, private element: ElementRef) 
    { }
    
    @Input("sp-nav-item")
    value?: number;
    
    ngOnInit(){
        this.pageNumber.subscribe(num => {
            if (num == this.value 
                && !this.element.nativeElement.classList.contains("_active")) 
            {
                this.element.nativeElement.classList.toggle("_active");
            }
            else if (num != this.value 
                && this.element.nativeElement.classList.contains("_active")) 
            {
                this.element.nativeElement.classList.toggle("_active");
            }
        });
    }
    
    @HostListener("click")
    UpdateValue(){
        if (this.value != null && !this.element.nativeElement.classList.contains("_active")){
            this.pageNumber.next(this.value);
            this.filter.pageNumber = this.value;
            this.products.getProducts();
        }
        
    }
    
}