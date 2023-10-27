import {Component, EventEmitter, Output} from '@angular/core';
import {NzModalRef} from "ng-zorro-antd/modal";

@Component({
    selector: 'app-base-plugin',
    templateUrl: './base-plugin.component.html',
    styleUrls: ['./base-plugin.component.css']
})
export class BasePluginComponent {

    @Output() confirmed: EventEmitter<void> = new EventEmitter();

    isSpinning: boolean = false;

    constructor(private modal: NzModalRef) {
    }

    cancelClicked() {
        this.destroyModal();
    }

    async confirmClicked() {
        this.confirmed.emit();
    }

    private destroyModal(): void {
        this.modal.destroy();
    }

    closeModal() {
        this.destroyModal();
    }

    startProgress() {
        this.isSpinning = true;
    }

    endProgress() {
        this.isSpinning = false;
    }
}
