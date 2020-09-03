import { Injectable } from '@angular/core';

@Injectable()
export class NavbarService {
    private visible: boolean;
    static instance: NavbarService;
    constructor() { this.visible = false; NavbarService.instance = this; }

    hide(): void { this.visible = false; }

    show(): void { this.visible = true; }

    toggle(): void { this.visible = !this.visible; }

    isVisible(): boolean { return this.visible; }

}