import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HeaderModule} from "./header/header.module";
import {FooterModule} from "./footer/footer.module";
import {ModelModule} from "./model/model.module";
import {StoreModule} from "./store/store.module";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";

@NgModule({
  declarations: [
      AppComponent
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HeaderModule,
        FooterModule,
        ModelModule,
        StoreModule,
        BrowserAnimationsModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
