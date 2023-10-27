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
import { PluginPropertiesComponent } from './plugin/plugin-properties/plugin-properties.component';
import {NzTabsModule} from "ng-zorro-antd/tabs";
import { PluginConfigTabComponent } from './plugin/plugin-config-tab/plugin-config-tab.component';
import {FormsModule} from "@angular/forms";
import {NzInputModule} from "ng-zorro-antd/input";
import {NzTimePickerModule} from "ng-zorro-antd/time-picker";
import {NzDatePickerModule} from "ng-zorro-antd/date-picker";
import {NzInputNumberModule} from "ng-zorro-antd/input-number";
import { BasePluginComponent } from './plugin/base-plugin/base-plugin.component';
import {NzButtonModule} from "ng-zorro-antd/button";
import {NzSpinModule} from "ng-zorro-antd/spin";
import {NzModalModule} from "ng-zorro-antd/modal";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";

@NgModule({
  declarations: [
    AppComponent,
    ScenarioDesignerComponent,
    PageNotFoundComponent,
    ScenarioPanelComponent,
    PluginBoxComponent,
    PluginsToolboxComponent,
    PluginToolboxComponent,
    PluginPropertiesComponent,
    PluginConfigTabComponent,
    BasePluginComponent
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
        NzInputNumberModule,
        NzButtonModule,
        NzSpinModule,
        NzModalModule,
        BrowserAnimationsModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
