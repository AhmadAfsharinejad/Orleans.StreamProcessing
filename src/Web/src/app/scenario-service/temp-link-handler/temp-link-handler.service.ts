import {Injectable} from '@angular/core';
import {Graph, Edge} from "@antv/x6";
import {v4 as uuidv4} from 'uuid';
import {GraphService} from "../graph/graph.service";

@Injectable({
    providedIn: 'root'
})
export class TempLinkHandlerService {
    private graph!: Graph;
    private container!: HTMLElement | null;
    private sourceNodeId!: string | null;
    private tempEdge!: Edge | null;
    private graphService!: GraphService;

    constructor() {
    }

    initGraph(graphService: GraphService, graph: Graph, graphContainerId: string): void {
        this.graph = graph;
        this.graphService = graphService;

        graph.on('node:click', ({x, y, node}) => this.nodeClicked(x, y, node.id));

        this.container = document.getElementById(graphContainerId);
    }

    nodeClicked(x: number, y: number, id: string): void {

        if (!this.sourceNodeId) {
            this.addTempLink(x, y, id);
            return;
        }

        if (this.sourceNodeId == id) {
            this.clear();
            return;
        }

        this.removeTempLink();
        this.addLink(id);
        this.clear();
    }

    addTempLink(x: number, y: number, sourceId: string): void {
        this.clear();
        this.sourceNodeId = sourceId;
        this.container!.addEventListener("mousemove", this.mouseMoveHandler);
        this.container!.addEventListener("mouseleave", this.mouseLeaveHandler);
        document.addEventListener('keyup', this.keyUpHandler);

        this.tempEdge = this.graph.addEdge({
            source: sourceId,
            target: {
                x: x,
                y: y
            }
        });

        //to stop conflict with double-clicked, we set visible to false. if mouse move ve set it to true
        this.tempEdge.visible = false;
    }

    moveTempLink(x: number, y: number): void {
        this.tempEdge!.setTarget({
            x: x,
            y: y
        });
        this.tempEdge!.visible = true;
    }

    removeTempLink(): void {
        if (!this.tempEdge) return;

        this.graph.removeEdge(this.tempEdge);
    }

    addLink(targetId: string): void {
        this.graphService.addLink(this.sourceNodeId!, targetId, uuidv4());
    }

    clear(): void {
        this.removeTempLink();
        this.sourceNodeId = null;
        this.tempEdge = null;
        this.container!.removeEventListener("mousemove", this.mouseMoveHandler);
        this.container!.removeEventListener("mouseleave", this.mouseLeaveHandler);
        document.removeEventListener('keyup', this.keyUpHandler);
    }

    mouseMoveHandler = (e: MouseEvent): void => {
        this.moveTempLink(e.offsetX, e.offsetY);
    };

    mouseLeaveHandler = (): void => {
        this.clear();
    };

    keyUpHandler = (e: KeyboardEvent): void => {

        if(e.key === "Escape"){
            this.clear();
        }
    };
}
