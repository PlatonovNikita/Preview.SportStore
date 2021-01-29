import {NgModule} from "@angular/core";
import {Message} from "../model/message/message.model";
import {MessageComponent} from "./message/message.component";
import {PopupComponent} from "./popup.component";
import {BrowserModule} from "@angular/platform-browser";
import {MessageService} from "../model/message/message.service";

@NgModule({
    declarations: [PopupComponent, MessageComponent],
    imports: [
        BrowserModule,
    ],
    providers: [MessageService]
})
export class PopupModule{
    
}