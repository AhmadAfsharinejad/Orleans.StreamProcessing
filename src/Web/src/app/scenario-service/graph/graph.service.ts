import {Injectable} from '@angular/core';
import {Graph} from "@antv/x6";
import {PluginTypeId} from "../../plugin-dto/plugin-type-id";
import {PluginId} from "../../plugin-dto/plugin-id";
import {PluginBoxProviderService} from "../../plugin-service/plugin-box-provider/plugin-box-provider.service";
import {TempLinkHandlerService} from "../temp-link-handler/temp-link-handler.service";
import {NodeConfigHandlerService} from "../node-config-handler/node-config-handler.service";

@Injectable({
    providedIn: 'root'
})
export class GraphService {
    private graph!: Graph;

    constructor(private pluginBoxProviderService: PluginBoxProviderService,
                private tempLinkHandlerService: TempLinkHandlerService,
                private nodeConfigHandlerService: NodeConfigHandlerService) {
    }

    initGraph(graph: Graph, graphContainerId: string): void {
        this.graph = graph;

        this.tempLinkHandlerService.initGraph(this, graph, graphContainerId);
        this.nodeConfigHandlerService.initGraph(graph);
    }

    addNode(pluginTypeId: PluginTypeId, pluginId: PluginId, x: number, y: number): void {

        let plugin = this.pluginBoxProviderService.getPlugin(pluginTypeId, pluginId);

        this.graph.addNode({
            shape: 'plugin-box-node',
            x: (x - 30),
            y: (y - 30),
            id: pluginId.value,
            label: 'world',
            width: 100,
            height: 100,
            data: {
                ngArguments: {
                    plugin: plugin
                }
            }
        });
    }

    addLink(source: string, target: string, linkId: string): void {
        this.graph.addEdge({
            source: source,
            target: target,
            id: linkId
        });
    }
}
