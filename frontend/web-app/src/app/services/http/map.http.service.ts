import { Injectable } from '@angular/core';
import * as L from 'leaflet';

@Injectable()
export class MapHttpService {

    protected mapTiles(): L.TileLayer {
        return L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; Floiir'
        });
    }

}
