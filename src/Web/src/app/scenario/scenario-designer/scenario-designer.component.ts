import {Component, OnInit} from '@angular/core';
import {Graph} from "@antv/x6";

@Component({
  selector: 'app-scenario-designer',
  templateUrl: './scenario-designer.component.html',
  styleUrls: ['./scenario-designer.component.css']
})
export class ScenarioDesignerComponent implements OnInit {
  private graph!: Graph;

  ngOnInit(): void {
    this.createGraph();
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

}
