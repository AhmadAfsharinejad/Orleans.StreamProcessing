import {Component, Input} from '@angular/core';
import {PluginBoxInfo} from "./plugin-box-info";

@Component({
  selector: 'app-plugin-box',
  templateUrl: './plugin-box.component.html',
  styleUrls: ['./plugin-box.component.css']
})
export class PluginBoxComponent {
  @Input() plugin!: PluginBoxInfo;
}
