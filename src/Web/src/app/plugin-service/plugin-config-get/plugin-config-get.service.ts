import {Injectable} from '@angular/core';
import {PluginUiType} from "../../plugin-dto/plugin-ui-type";
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
            propertiesTabs: [{
                properties: [{
                    name: 'c1',
                    type: PluginUiType.BOOLEAN,
                    value: true
                },
                    {
                        name: 'c2222222222',
                        type: PluginUiType.INTEGER,
                        value: 12
                    }],
                name: 't1'
            }, {
                properties: [{
                    name: 'c1',
                    type: PluginUiType.TEXT,
                    value: 'Hi'
                },
                    {
                        name: 'c2',
                        type: PluginUiType.FLOAT,
                        value: 12.2
                    }],
                name: 'tab2222'
            }]
        };
    }
}
