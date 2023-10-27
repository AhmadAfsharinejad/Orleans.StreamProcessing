import {PluginPropertiesTab} from "./plugin-properties-tab";
import {PluginId} from "./plugin-id";

export interface PluginConfig {
    propertiesTabs: PluginPropertiesTab[];
    id: PluginId;
}
