import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from "@angular/router";
import {PageNotFoundComponent} from "./page-not-found/page-not-found.component";
import {ScenarioPanelComponent} from "./scenario/scenario-panel/scenario-panel.component";

const routes: Routes = [
  {path: 'Scenario', component: ScenarioPanelComponent, title: 'Scenario'},
  {path: '', redirectTo: '/Scenario', pathMatch: "full"},
  {path: '**', component: PageNotFoundComponent}
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ]
})
export class AppRoutingModule { }
