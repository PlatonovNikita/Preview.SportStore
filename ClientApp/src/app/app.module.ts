import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HeaderModule} from "./header/header.module";
import {FooterModule} from "./footer/footer.module";
import {ModelModule} from "./model/model.module";
import {StoreModule} from "./store/store.module";

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
        StoreModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
