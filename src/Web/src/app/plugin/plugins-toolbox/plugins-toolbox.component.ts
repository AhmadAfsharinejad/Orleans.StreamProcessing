import {Component, OnInit} from '@angular/core';
import {PluginToolboxInfo} from "../../plugin-dto/plugin-toolbox-info";
import {PluginTypeId} from "../../plugin-dto/plugin-type-id";
import {ToolboxProviderService} from "../../plugin-service/toolbox-provider/toolbox-provider.service";

@Component({
  selector: 'app-plugins-toolbox',
  templateUrl: './plugins-toolbox.component.html',
  styleUrls: ['./plugins-toolbox.component.css']
})
export class PluginsToolboxComponent implements OnInit{
  plugins?: PluginToolboxInfo[];

  constructor(private toolboxProviderService: ToolboxProviderService) {
  }

  async ngOnInit(): Promise<void> {
    this.plugins = await this.toolboxProviderService.getPlugins();
  }
  onPluginDragStarted($event: DragEvent, pluginTypeId: PluginTypeId) {
    $event.dataTransfer!.setData("text/plugin-type-id", pluginTypeId.value);
    //TODO handle event
  }
}
