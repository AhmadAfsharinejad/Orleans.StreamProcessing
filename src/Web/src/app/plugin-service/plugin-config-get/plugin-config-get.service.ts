import {Injectable} from '@angular/core';
import {PluginConfigTab} from "../../plugin-dto/plugin-config-tab";
import {PluginConfigType} from "../../plugin-dto/plugin-config-type";
import {PluginId} from "../../plugin-dto/plugin-id";
import {PluginConfig} from "../../plugin-dto/plugin-config";

@Injectable({
    providedIn: 'root'
})
export class PluginConfigGetService {

    constructor() {
    }

    async get(id: PluginId): Promise<PluginConfig> {
        //TODO
        return {
            id: id,
            configTabs: [{
                properties: [{
                    name: 'c1',
                    type: PluginConfigType.BOOLEAN,
                    value: true
                },
                    {
                        name: 'c2222222222',
                        type: PluginConfigType.INTEGER,
                        value: 12
                    }],
                name: 't1'
            }, {
                properties: [{
                    name: 'c1',
                    type: PluginConfigType.TEXT,
                    value: 'Hi'
                },
                    {
                        name: 'c2',
                        type: PluginConfigType.FLOAT,
                        value: 12.2
                    }],
                name: 'tab2222'
            }]
        };
    }
}
