import { Component, AfterViewInit, ElementRef } from '@angular/core';
import { MapManager } from './map-manager';
import * as L from 'leaflet';
import { NavbarService } from 'src/app/services/navbar.service';
const iconRetinaUrl = 'assets/marker-icon-2x.png';
const iconUrl = 'assets/marker-icon.png';
const shadowUrl = 'assets/marker-shadow.png';
const iconDefault = L.icon({
  iconRetinaUrl,
  iconUrl,
  shadowUrl,
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  tooltipAnchor: [16, -28],
  shadowSize: [41, 41]
});
L.Marker.prototype.options.icon = iconDefault;


@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements AfterViewInit {

  constructor(public navbarService: NavbarService) {
    this.mapManager = new MapManager(this.map);
  }
  private map;
  mapManager: MapManager;
  popup = L.popup();
  searchValue = '';
  isMenuButtonDisabled = false;

  ngAfterViewInit(): void {
    this.navbarService.hide();

    this.mapManager.initMap();

    // test stuff
    this.mapManager.addMarker(8, 4);
    this.mapManager.addCircle(1, 2, {
      radius: 100,
      color: 'blue'
    }, 'circle');
    this.mapManager.addPolygon([[1, 1], [3, 1], [3, 3], [1, 3]]);
    this.mapManager.addEvent('click');
  }

  menuToggle(): void {
    if (!this.isMenuButtonDisabled) {
      this.navbarService.toggle();
      this.isMenuButtonDisabled = true;
      setTimeout(() => {
        this.isMenuButtonDisabled = false;

      }, 2000);
    }
  }




}
