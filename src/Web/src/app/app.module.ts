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
import { PluginConfigComponent } from './plugin/plugin-config/plugin-config.component';
import {NzTabsModule} from "ng-zorro-antd/tabs";
import { PluginConfigTabComponent } from './plugin/plugin-config-tab/plugin-config-tab.component';
import {FormsModule} from "@angular/forms";
import {NzInputModule} from "ng-zorro-antd/input";
import {NzTimePickerModule} from "ng-zorro-antd/time-picker";
import {NzDatePickerModule} from "ng-zorro-antd/date-picker";
import {NzInputNumberModule} from "ng-zorro-antd/input-number";

@NgModule({
  declarations: [
    AppComponent,
    ScenarioDesignerComponent,
    PageNotFoundComponent,
    ScenarioPanelComponent,
    PluginBoxComponent,
    PluginsToolboxComponent,
    PluginToolboxComponent,
    PluginConfigComponent,
    PluginConfigTabComponent
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        NgOptimizedImage,
        RouterOutlet,
        NzTabsModule,
        FormsModule,
        NzInputModule,
        NzTimePickerModule,
        NzDatePickerModule,
        NzInputNumberModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
