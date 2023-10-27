import {PluginConfigType} from "./plugin-config-type";

export interface PluginProperty {
    name: string;
    value: any;
    type: PluginConfigType;
}
