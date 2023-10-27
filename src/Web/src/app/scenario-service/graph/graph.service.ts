import {Injectable} from '@angular/core';
import {Graph} from "@antv/x6";
import {PluginTypeId} from "../../plugin-dto/plugin-type-id";
import {PluginId} from "../../plugin-dto/plugin-id";
import {PluginBoxProviderService} from "../../plugin-service/plugin-box-provider/plugin-box-provider.service";

@Injectable({
  providedIn: 'root'
})
export class GraphService {
  private graph!: Graph;

  constructor(private pluginBoxProviderService: PluginBoxProviderService) {
  }

  init(graph: Graph): void {
    this.graph = graph;
  }

  addNode(pluginTypeId: PluginTypeId, pluginId: PluginId, x: number, y: number): void {

    let plugin = this.pluginBoxProviderService.getPlugin(pluginTypeId, pluginId);

    this.graph.addNode({
      shape: 'plugin-box-node',
      x: (x - 30),
      y: (y - 30),
      id: pluginId.value,
      label: 'world',
      width: 60,
      height: 60,
      data: {
        ngArguments: {
          plugin: plugin
        }
      }
    });
  }
}
