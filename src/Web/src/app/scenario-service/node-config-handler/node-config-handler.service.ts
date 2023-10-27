import {Injectable} from '@angular/core';
import {Graph} from "@antv/x6";
import {PluginConfigShowService} from "../../plugin-service/plugin-config-show/plugin-config-show.service";

@Injectable({
    providedIn: 'root'
})
export class NodeConfigHandlerService {
    private graph!: Graph;

    constructor(private pluginConfigShowService: PluginConfigShowService) {
    }

    initGraph(graph: Graph): void {
        this.graph = graph;

        graph.on('node:dblclick', ({node}) => this.pluginConfigShowService.show({value: node.id}));
    }
}
