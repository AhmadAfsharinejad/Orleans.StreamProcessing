import {Injectable} from '@angular/core';
import {PluginMetadataInfo} from "../../plugin-dto/plugin-metadata-info";
import {PluginTypeId} from "../../plugin-dto/plugin-type-id";

@Injectable({
  providedIn: 'root'
})
export class PluginMetadataProviderService {
  plugins!: PluginMetadataInfo[];

  constructor() {
  }

  async getPlugins(): Promise<PluginMetadataInfo[]> {

    if (this.plugins) return this.plugins;

    this.plugins = await this.fetchPlugins();

    return this.plugins;
  }

  getPlugin(typeId: PluginTypeId) : PluginMetadataInfo{
    return this.plugins.find(x => x.pluginTypeId.value == typeId.value)!;
  }

  private async fetchPlugins(): Promise<PluginMetadataInfo[]> {
    return [
      {
        name: 'Http Listener',
        iconPath: 'assets/img/plugin-icons/http-listener.png',
        pluginTypeId: {value: 'HttpListener'}
      },
      {name: 'Map', iconPath: 'assets/img/plugin-icons/map.png', pluginTypeId: {value: 'Map'}},
      {name: 'Filter', iconPath: 'assets/img/plugin-icons/filter.png', pluginTypeId: {value: 'Filter'}},
      {
        name: 'Sql Executor',
        iconPath: 'assets/img/plugin-icons/sql-executor.png',
        pluginTypeId: {value: 'SqlExecutor'}
      },
      {name: 'Rest', iconPath: 'assets/img/plugin-icons/rest.png', pluginTypeId: {value: 'Rest'}},
      {
        name: 'Http Response',
        iconPath: 'assets/img/plugin-icons/http-response.png',
        pluginTypeId: {value: 'HttpResponse'}
      }
    ];//TODO call engine
  }
}
