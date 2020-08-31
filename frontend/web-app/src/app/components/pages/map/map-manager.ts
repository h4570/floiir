import * as L from 'leaflet';
import { NavbarService } from 'src/app/services/navbar.service';


export class MapManager {

    private map = null;
    constructor(map) {
        this.map = map;
    }

    public initMap(): void {
        this.map = L.map('map', {
            center: [39.8282, -98.5795],
            zoom: 3,
            zoomControl: false
        });
        const tiles = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; Floiir'
        });
        L.control.zoom({
            position: 'bottomleft'
        }).addTo(this.map);

        tiles.addTo(this.map);
    }

    public addMarker(x: number, y: number): void { // TODO add type of marker
        L.marker([39.8282, -98.5795]).addTo(this.map);
    }

    public addCircle(x: number, y: number, options: L.CircleMarkerOptions, tooltip?: string, popout?: string): void {
        const circle = L.circleMarker([x, y], options);
        if (tooltip !== undefined) {
            circle.bindTooltip(`<div><h4>${tooltip}</h4></div>`);
        }
        if (popout !== undefined) {
            circle.bindPopup(`<div> <h3>${popout}</h3> </div>`);
        }
        circle.addTo(this.map);
    }

    public addPolygon(parameters: L.LatLngExpression[], options?: L.PolylineOptions, tooltip?: string, popout?: string): void {
        const polygon = L.polygon(parameters, options);
        if (tooltip !== undefined) {
            polygon.bindTooltip(`<div><h4>${tooltip}</h4></div>`);
        }
        if (popout !== undefined) {
            polygon.bindPopup(`<div> <h3>${popout}</h3> </div>`);
        }
        polygon.addTo(this.map);
    }


    onMapClick(e): void {
        // alert("You clicked the map at " + e.latlng.toString());
    }
    public addEvent(): void {
        this.map.on('click', this.onMapClick);
    }

}