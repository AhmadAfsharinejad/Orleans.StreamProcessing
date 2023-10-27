import {Injectable} from '@angular/core';
import {PluginId} from "../../plugin-dto/plugin-id";
import {PluginConfigGetService} from "../plugin-config-get/plugin-config-get.service";
import {NzModalService} from "ng-zorro-antd/modal";
import {PluginConfigTabComponent} from "../../plugin/plugin-config-tab/plugin-config-tab.component";

@Injectable({
    providedIn: 'root'
})
export class PluginConfigShowService {

    constructor(private modalService: NzModalService,
                private pluginConfigGetService: PluginConfigGetService) {
    }

    async show(id: PluginId): Promise<void> {

        let config = await this.pluginConfigGetService.get(id);

        this.modalService.create({
            nzTitle: 'cnf',
            nzContent: PluginConfigTabComponent,
            nzFooter: null,
            nzClosable: false,
            nzMaskClosable: false,
            nzWidth: 600,
            nzStyle: {},
            nzBodyStyle: {padding: '5px', height: '350px'},
            nzData: config,
        });
    }
}
