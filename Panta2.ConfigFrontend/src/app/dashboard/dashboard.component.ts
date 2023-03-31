import { Component, OnInit } from "@angular/core";
import { Service } from "../core/models/service.model";

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.sass']
})
export class DashboardComponent implements OnInit {
  services: Service[] = [];
  favoriteServices: Service[] = [];
  filteredServices: Service[] = [];
  isLoading: boolean = true;

  constructor() { }

  async ngOnInit(): Promise<void> {
    this.isLoading = false;
  }

  onFiltered(filteredItems: any[]) {
    this.filteredServices = filteredItems;

    console.log(this.filteredServices);
  }
} 