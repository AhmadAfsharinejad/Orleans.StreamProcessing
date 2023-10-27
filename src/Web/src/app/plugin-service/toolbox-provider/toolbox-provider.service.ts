import {Injectable} from '@angular/core';
import {PluginToolboxInfo} from "../../plugin-dto/plugin-toolbox-info";
import {PluginMetadataProviderService} from "../plugin-metadata-provider/plugin-metadata-provider.service";
import {PluginMetadataInfo} from "../../plugin-dto/plugin-metadata-info";

@Injectable({
  providedIn: 'root'
})
export class ToolboxProviderService {

  constructor(private pluginMetadataProviderService: PluginMetadataProviderService) {
  }

  async getPlugins(): Promise<PluginToolboxInfo[]> {

    let plugins = await this.pluginMetadataProviderService.getPlugins();
    return plugins.map(x => this.map(x));
  }

  map(info: PluginMetadataInfo): PluginToolboxInfo {
    return {displayName: info.name, pluginTypeId: info.pluginTypeId, iconPath: info.iconPath};
  }
}
