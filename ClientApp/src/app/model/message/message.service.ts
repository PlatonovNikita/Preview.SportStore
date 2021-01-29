import {Injectable} from "@angular/core";
import {Observable, Subject} from "rxjs";
import {Message} from "./message.model";

@Injectable()
export class MessageService {
    messageService = new Subject<Message>();

    reportMessage(message: Message){
        this.messageService.next(message);
    }

    getObservable(): Observable<Message>{
        return this.messageService;
    }
}