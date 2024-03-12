import { ComponentRef, ViewRef } from "@angular/core";

export interface ITab{
    header : string
    uniqueCode : string 
    content: ComponentRef<any>
    view: ViewRef
}

export interface ITabComponent{
    IsActive : boolean;
}