import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {TableComponent} from "./store/table/table.component";

const routes: Routes = [
    {path: "table", component: TableComponent},
    {path: "table/:category", component: TableComponent},
    {path: "", redirectTo: "/table", pathMatch: "full"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
