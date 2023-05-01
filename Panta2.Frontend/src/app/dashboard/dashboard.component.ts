import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { StorageService } from "../core/services/storage/storage.service";
import { UserService } from "../core/services/user/user.service";
import { User } from "../core/models/user.model";
import { firstValueFrom } from 'rxjs';
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
  userName?: string;
  companyLogo?: string;
  isLoading: boolean = true;

  constructor(private storageService: StorageService, private userService: UserService) { }

  async ngOnInit(): Promise<void> {
    try {
      const user: User = await this.storageService.getUser();

      this.services = await firstValueFrom(this.userService.getServices(user.id));
      this.favoriteServices = await firstValueFrom(this.userService.getFavoriteServices(user.id));
      this.filteredServices = this.services;
      this.isLoading = false;

    } catch (error) {
      console.error(error);
      this.storageService.deleteUserStorage();
    }
  }

  onFiltered(filteredItems: any[]) {
    this.filteredServices = filteredItems;

    console.log(this.filteredServices);
  }
} 