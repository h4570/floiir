import * as L from 'leaflet';
import { MapService, MapGetResponse } from 'src/app/services/map.service';


export class MapManager {

    private map = null;
    public mapHttpService = new MapService();
    constructor(map) {
        this.map = map;
    }

    /* set options of map and get tiles from API */
    public initMap(): void {
        this.map = L.map('map', {
            center: [39.8282, -98.5795],
            zoom: 3,
            zoomControl: false
        });
        this.mapHttpService.getTiles().then(response => {
            if (response.responseCode === MapGetResponse.Success) {
                const tiles = response.data;

                tiles.addTo(this.map);
            }
        });
        L.control.zoom({
            position: 'bottomleft'
        }).addTo(this.map);
    }


    /* Draw marker on map at X,Y cords*/
    public addMarker(x: number, y: number): void { // TODO add type of marker
        L.marker([39.8282, -98.5795]).addTo(this.map);
    }

    /* Draw circle on map at X,Y cords and provided options*/
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

    /* Draw custom polygon on map by array of cords and provided options*/
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

    /* Add a custom event on specific action and function */
    public addEvent(action: string): void {
        this.map.on(action, this.onMapClick);
    }

}