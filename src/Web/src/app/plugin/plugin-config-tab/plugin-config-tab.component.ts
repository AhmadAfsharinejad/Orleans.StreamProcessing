import {Component, Input, OnInit} from '@angular/core';
import {PluginConfigTab} from "../../plugin-dto/plugin-config-tab";
import {PluginConfigType} from "../../plugin-dto/plugin-config-type";

@Component({
  selector: 'app-plugin-config-tab',
  templateUrl: './plugin-config-tab.component.html',
  styleUrls: ['./plugin-config-tab.component.css']
})
export class PluginConfigTabComponent implements OnInit {
@Input() pluginTabs!:PluginConfigTab[];

//TODO
  ngOnInit(): void {
    this.pluginTabs = [{
      configs: [{
        name: 'c1',
        type : PluginConfigType.BOOLEAN,
        value: true
      },
        {
          name: 'c2222222222',
          type : PluginConfigType.NUMBER,
          value: 12
        }],
      name: 't1'
    },{
      configs: [{
        name: 'c1',
        type : PluginConfigType.TEXT,
        value: 'sdasda'
      },
        {
          name: 'c2',
          type : PluginConfigType.NUMBER,
          value: 12.2
        }],
      name: 'tab2222'
    }];
  }
}
