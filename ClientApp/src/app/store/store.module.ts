import {NgModule} from "@angular/core";
import {TableComponent} from "./table/table.component";
import {MenuPageComponent} from "./table/menuPage.component";
import {BrowserModule} from "@angular/platform-browser";
import {RouterModule} from "@angular/router";

@NgModule({
    imports: [
        BrowserModule,
        RouterModule
    ],
    declarations: [TableComponent, MenuPageComponent]
})
export class StoreModule {}