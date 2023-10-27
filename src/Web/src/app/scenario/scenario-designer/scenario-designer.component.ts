import {AfterViewInit, Component, Injector, OnInit} from '@angular/core';
import {Graph} from "@antv/x6";
import {register} from '@antv/x6-angular-shape';
import {PluginBoxComponent} from "../../plugin/plugin-box/plugin-box.component";
import {GraphService} from "../../scenario-service/graph/graph.service";
import { v4 as uuidv4 } from 'uuid';

@Component({
  selector: 'app-scenario-designer',
  templateUrl: './scenario-designer.component.html',
  styleUrls: ['./scenario-designer.component.css']
})
export class ScenarioDesignerComponent implements OnInit, AfterViewInit {
  private graph!: Graph;

  constructor(private injector: Injector, private graphService: GraphService) {
  }

  ngOnInit(): void {
    this.createGraph();
  }

  ngAfterViewInit(): void {
    this.registerShape();

    this.graphService.initGraph(this.graph!, 'container');
  }

  registerShape(): void {
    register({
      shape: 'plugin-box-node',
      content: PluginBoxComponent,
      injector: this.injector,
    })
  }

  createGraph(): void {
    this.graph = new Graph({
      container: document.getElementById('container')!,
      autoResize: true,
      grid: true,
      connecting: {
        allowBlank: false
      },
      background: {
        color: '#afafaf',
      },
    });
  }

  onDragOver($event: DragEvent) {
    $event.preventDefault();
  }

  onDrop($event: DragEvent) {
    $event.preventDefault();
    let data = $event.dataTransfer!.getData("text/plugin-type-id");
    if (!data) return;

    this.graphService.addNode( {value: data}, {value: uuidv4()}, $event.offsetX, $event.offsetY);
  }
}
