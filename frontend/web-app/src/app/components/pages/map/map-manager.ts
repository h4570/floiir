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

    public addMarker(): void {
        L.marker([39.8282, -98.5795]).addTo(this.map);
    }

    public addCircle(): void {
        const circle = L.circleMarker([75, 25], {
            radius: 100,
            color: 'yellow'

        });
        circle.bindPopup('<div> <h3>circle</h3> </div>');
        circle.bindTooltip('<div><h4>Click me</h4></div>');
        circle.addTo(this.map);
    }

    public addPolygon(): void {
        const square = L.polygon([[1, 1], [3, 1], [3, 3], [1, 3]], {
            color: 'red',
            fillColor: 'green'
        });
        square.bindPopup('<div> <h3>square</h3> </div>');
        square.bindTooltip('<div><h4>Click me</h4></div>');
        square.addTo(this.map);
    }


    onMapClick(e): void {
        // alert("You clicked the map at " + e.latlng.toString());
    }
    public addEvent(): void {
        this.map.on('click', this.onMapClick);
    }

}