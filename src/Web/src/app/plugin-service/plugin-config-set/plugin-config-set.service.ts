import {Injectable} from '@angular/core';
import {PluginConfig} from "../../plugin-dto/plugin-config";

@Injectable({
    providedIn: 'root'
})
export class PluginConfigSetService {

    constructor() {
    }

    async set(config: PluginConfig): Promise<void> {
        console.log(config);
        //TODO
    }
}
