import {PluginConfigType} from "./plugin-config-type";

export interface PluginConfig {
    name: string;
    value: any;
    type: PluginConfigType;
}
