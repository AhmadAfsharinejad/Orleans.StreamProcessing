import {PluginTypeId} from "../../plugin-dto/plugin-type-id";
import {PluginId} from "../../plugin-dto/plugin-id";

export interface PluginBoxInfo {
    label: string;
    pluginTypeId: PluginTypeId;
    iconPath: string;
    id: PluginId;
}
