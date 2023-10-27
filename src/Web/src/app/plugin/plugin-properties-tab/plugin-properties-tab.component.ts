import {Component, inject, ViewChild} from '@angular/core';
import {BasePluginComponent} from "../base-plugin/base-plugin.component";
import {NZ_MODAL_DATA} from "ng-zorro-antd/modal";
import {PluginConfigSetService} from "../../plugin-service/plugin-config-set/plugin-config-set.service";
import {PluginConfig} from "../../plugin-dto/plugin-config";

@Component({
    selector: 'app-plugin-properties-tab',
    templateUrl: './plugin-properties-tab.component.html',
    styleUrls: ['./plugin-properties-tab.component.css']
})
export class PluginPropertiesTabComponent {
    readonly config: PluginConfig = inject(NZ_MODAL_DATA);

    @ViewChild(BasePluginComponent) child!: BasePluginComponent;

    constructor(private pluginConfigSetService: PluginConfigSetService) {
    }

    async confirmed() {
        this.child.startProgress();

        try {
            await this.pluginConfigSetService.set(this.config);
            this.child.closeModal();
        } catch (error) {
            //TODO
        } finally {
            this.child.endProgress();
        }
    }
}
