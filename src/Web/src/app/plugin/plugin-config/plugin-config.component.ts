import {Component, Input} from '@angular/core';
import {PluginConfig} from "../../plugin-dto/plugin-config";
import {PluginConfigType} from "../../plugin-dto/plugin-config-type";

@Component({
    selector: 'app-plugin-config',
    templateUrl: './plugin-config.component.html',
    styleUrls: ['./plugin-config.component.css']
})
export class PluginConfigComponent {
    @Input() configs!: PluginConfig[];
    protected readonly PluginConfigType = PluginConfigType;
}
