import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { ScenarioDesignerComponent } from './scenario/scenario-designer/scenario-designer.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { ScenarioPanelComponent } from './scenario/scenario-panel/scenario-panel.component';
import { PluginBoxComponent } from './plugin/plugin-box/plugin-box.component';
import { PluginsToolboxComponent } from './plugin/plugins-toolbox/plugins-toolbox.component';
import { PluginToolboxComponent } from './plugin/plugin-toolbox/plugin-toolbox.component';
import {NgOptimizedImage} from "@angular/common";
import {RouterOutlet} from "@angular/router";

@NgModule({
  declarations: [
    AppComponent,
    ScenarioDesignerComponent,
    PageNotFoundComponent,
    ScenarioPanelComponent,
    PluginBoxComponent,
    PluginsToolboxComponent,
    PluginToolboxComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgOptimizedImage,
    RouterOutlet
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
