import {Component, Input} from '@angular/core';
import {PluginToolboxInfo} from "../../plugin-dto/plugin-toolbox-info";

@Component({
  selector: 'app-plugin-toolbox',
  templateUrl: './plugin-toolbox.component.html',
  styleUrls: ['./plugin-toolbox.component.css']
})
export class PluginToolboxComponent {
  @Input() plugin!: PluginToolboxInfo;

}
