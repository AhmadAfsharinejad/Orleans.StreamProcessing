import {Component, Input} from '@angular/core';
import {PluginProperty} from "../../plugin-dto/plugin-property";
import {PluginUiType} from "../../plugin-dto/plugin-ui-type";

@Component({
    selector: 'app-plugin-properties',
    templateUrl: './plugin-properties.component.html',
    styleUrls: ['./plugin-properties.component.css']
})
export class PluginPropertiesComponent {
    @Input() properties!: PluginProperty[];
    protected readonly PluginUiType = PluginUiType;
}
