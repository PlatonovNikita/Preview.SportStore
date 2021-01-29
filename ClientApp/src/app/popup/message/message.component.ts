import {Component, Injectable} from "@angular/core";
import {Message} from "../../model/message/message.model";
import {Observable, Subject} from "rxjs";
import {MessageService} from "../../model/message/message.service";

@Component({
    selector: "spMessage",
    templateUrl: "message.component.html"
})
export class MessageComponent {
    message: Message;
    
    constructor(private messageService: MessageService) {
        messageService.getObservable().subscribe(m => this.message = m);
    }
    
    get lastMessage(): Message{
        return this.message;
    }
}