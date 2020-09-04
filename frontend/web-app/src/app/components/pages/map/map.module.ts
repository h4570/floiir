import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MapComponent } from './map.component';
import { AppHistoryService } from 'src/app/services/app-history.service';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '../../shared/shared.module';
import { LeafletModule } from '@asymmetrik/ngx-leaflet';
import * as L from 'leaflet';
import { NavbarService } from 'src/app/services/navbar.service';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
    declarations: [
        MapComponent
    ],
    imports: [
        CommonModule,
        TranslateModule,
        SharedModule,
        LeafletModule,
        MatInputModule,
        MatFormFieldModule,
        MatIconModule,
        FormsModule,
        MatButtonModule
    ],
    exports: [
        MapComponent
    ],
    providers: [
        AppHistoryService,
        NavbarService
    ]
})
export class MapModule { }
