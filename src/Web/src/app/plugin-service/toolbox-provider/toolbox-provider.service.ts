import {Injectable} from '@angular/core';
import {PluginToolboxInfo} from "../../plugin-dto/plugin-toolbox-info";

@Injectable({
    providedIn: 'root'
})
export class ToolboxProviderService {

    constructor() {
    }

    async getPlugins(): Promise<PluginToolboxInfo[]> {
        return [
            {displayName: 'Http Listener', iconPath: 'assets/img/plugin-icons/http-listener.png', pluginTypeId: {value: 'HttpListener'}},
            {displayName: 'Map', iconPath: 'assets/img/plugin-icons/map.png', pluginTypeId: {value: 'Map'}},
            {displayName: 'Filter', iconPath: 'assets/img/plugin-icons/filter.png', pluginTypeId: {value: 'Filter'}},
            {displayName: 'Sql Executor', iconPath: 'assets/img/plugin-icons/sql-executor.png', pluginTypeId: {value: 'SqlExecutor'}},
            {displayName: 'Rest', iconPath: 'assets/img/plugin-icons/rest.png', pluginTypeId: {value: 'Rest'}},
            {displayName: 'Http Response', iconPath: 'assets/img/plugin-icons/http-response.png', pluginTypeId: {value: 'HttpResponse'}}
        ];//TODO call engine
    }
}
