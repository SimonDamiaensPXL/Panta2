import { Component, OnInit } from "@angular/core";
import { Company } from "../core/models/company.model";

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
  companies: Company[] = [];
  isLoading: boolean = true;

  constructor() { }

  async ngOnInit(): Promise<void> {
    this.isLoading = false;
  }

  onFiltered(filteredItems: any[]) {
  }
} 