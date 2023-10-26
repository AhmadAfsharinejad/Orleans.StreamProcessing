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

    onDragOver($event: DragEvent) {
        $event.preventDefault();
    }

    onDrop($event: DragEvent) {
        $event.preventDefault();
        let data = $event.dataTransfer!.getData("text/plugin-type-id");
        if (!data) return;
        console.log($event);

        this.addNode($event.offsetX, $event.offsetY);
    }

    addNode(x: number, y: number): void {
        this.graph.addNode({
            shape: 'rect',
            x: x,
            y: y,
            label: 'world',
            width: 100,
            height: 40,
        });
    }
}
