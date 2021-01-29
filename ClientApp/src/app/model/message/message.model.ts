export class Message{
    
    constructor(public text: string,
                public danger: boolean,
                public responses?: [[string, (text:string) => void]]) {
    }
}