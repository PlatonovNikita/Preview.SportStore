import {InjectionToken, NgModule} from "@angular/core";
import {TableComponent} from "./table/table.component";
import {BrowserModule} from "@angular/platform-browser";
import {RouterModule} from "@angular/router";
import { MenuPageComponent } from './table/menu.page/menuPage.component';
import { SpStrFilterDirectory } from './table/filter/str.filter/str.filter.directory';
import {CLEAR_EMITTER, FilterComponent, SUBMIT_EMITTER} from './table/filter/filter.component';
import { SpBoolFilterDirectory } from './table/filter/bool.filter/bool.filter.directory';
import { SpNumberFilterDirective } from './table/filter/number.filter/number.filter.directive';
import { SpPriceFilter } from './table/filter/price.filter/price.filter.directory';
import {BehaviorSubject, Subject} from "rxjs";
import {SpAnimate} from "./table/animations/animate.directive";
import {SpTitleSection} from "./table/animations/title.section.directive";
import {SpBodySection} from "./table/animations/body.section.directive";
import {PAGE_NUMBER, PageSettingsComponent} from "./table/page.settings/page.settings.component";
import {CatalogProductsComponent} from "./table/catalog.products/catalog.products.component";
import {OrderByComponent} from "./table/order.by/order.by.component";
import {SpNavItem} from "./table/page.settings/nav.item/nav.item.directive";
import {FormsModule} from "@angular/forms";
import {SpPageSize} from "./table/page.settings/page.size/page.size.directive";
import {SpOptionSize} from "./table/page.settings/page.size/option.size.directive";

@NgModule({
    imports: [
        BrowserModule,
        RouterModule,
        FormsModule
    ],
    declarations: [TableComponent, MenuPageComponent,
        FilterComponent, SpStrFilterDirectory,
        SpBoolFilterDirectory, SpNumberFilterDirective,
        SpPriceFilter, SpAnimate, SpTitleSection, SpBodySection, 
        PageSettingsComponent, CatalogProductsComponent, OrderByComponent,
        SpNavItem, SpPageSize, SpOptionSize],
    providers: [{ provide: CLEAR_EMITTER, useValue: new Subject<void>()},
        { provide: SUBMIT_EMITTER, useValue: new Subject<void>()},
        { provide: PAGE_NUMBER, useValue: new BehaviorSubject<number>(1)}]
})
export class StoreModule {}