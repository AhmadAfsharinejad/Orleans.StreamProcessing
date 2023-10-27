import {Injectable} from '@angular/core';
import {PluginBoxInfo} from "../../plugin/plugin-box/plugin-box-info";
import {PluginTypeId} from "../../plugin-dto/plugin-type-id";
import {PluginId} from "../../plugin-dto/plugin-id";
import {PluginMetadataProviderService} from "../plugin-metadata-provider/plugin-metadata-provider.service";

@Injectable({
  providedIn: 'root'
})
export class PluginBoxProviderService {

  constructor(private pluginMetadataProviderService: PluginMetadataProviderService) {
  }

  getPlugin(pluginTypeId: PluginTypeId, id: PluginId): PluginBoxInfo {

    let plugin = this.pluginMetadataProviderService.getPlugin(pluginTypeId);

    return {pluginTypeId: pluginTypeId, id: id, label: plugin.name, iconPath: plugin.iconPath};
  }
}
