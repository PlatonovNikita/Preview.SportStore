import {InjectionToken, NgModule} from "@angular/core";
import {TableComponent} from "./table/table.component";
import {BrowserModule} from "@angular/platform-browser";
import {RouterModule} from "@angular/router";
import { MenuPageComponent } from './table/menu.page/menuPage.component';
import { SpStrFilterDirectory } from './table/filter/str.filter/str.filter.directory';
import { FilterComponent } from './table/filter/filter.component';
import { SpBoolFilterDirectory } from './table/filter/bool.filter/bool.filter.directory';
import { SpNumberFilterDirective } from './table/filter/number.filter/number.filter.directive';
import { SpPriceFilter } from './table/filter/price.filter/price.filter.directory';
import {Subject} from "rxjs";
import {SpAnimate} from "./table/animations/animate.directive";
import {SpTitleSection} from "./table/animations/title.section.directive";
import {SpBodySection} from "./table/animations/body.section.directive";

export const CLEAR_EMITTER = new InjectionToken("clear_emitter");

@NgModule({
    imports: [
        BrowserModule,
        RouterModule
    ],
    declarations: [TableComponent, MenuPageComponent, 
        FilterComponent, SpStrFilterDirectory,
        SpBoolFilterDirectory, SpNumberFilterDirective,
        SpPriceFilter, SpAnimate, SpTitleSection, SpBodySection],
    providers: [{ provide: CLEAR_EMITTER, useValue: new Subject<void>()}]
})
export class StoreModule {}