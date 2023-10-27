import {PluginConfigTab} from "./plugin-config-tab";
import {PluginId} from "./plugin-id";

export interface PluginConfig {
    configTabs: PluginConfigTab[];
    id: PluginId;
}
