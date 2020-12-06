import {Component} from "@angular/core";
import {ActivatedRoute} from "@angular/router";

@Component({
    selector: "spTable",
    templateUrl: "table.component.html"
})
export class TableComponent {
    constructor(private activeRoute: ActivatedRoute) {
        activeRoute.params.subscribe(params => {
            console.log(params["category"]);
        });
    }
}