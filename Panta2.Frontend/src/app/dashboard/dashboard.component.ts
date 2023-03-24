import { Component, OnInit, AfterViewInit } from "@angular/core";
import { Router } from "@angular/router";
import { StorageService } from "../core/services/storage/storage.service";
import { UserService } from "../core/services/user/user.service";
import { User } from "../core/models/user.model";
import { firstValueFrom } from 'rxjs';
import { ServiceService } from "../core/services/service/service.service";
import { Service } from "../core/models/service.model";

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.sass']
})
export class DashboardComponent implements OnInit, AfterViewInit  {
  services: Service[] = [];
  favoriteServices: Service[] = [];
  userName?: string;
  isLoading: boolean = true;

  constructor(private storageService: StorageService, private userService: UserService, private serviceService: ServiceService, private router: Router) { }

  async ngOnInit(): Promise<void> {
    try {
      const user: User = await this.storageService.getUser();
      this.userName = user.firstName;
      this.services = await firstValueFrom(this.serviceService.getServices(user.id));
      this.favoriteServices = await firstValueFrom(this.serviceService.getFavoriteServices(user.id));
    } catch (error) {
      console.error(error);
    }
  }

  ngAfterViewInit(): void {
    this.isLoading = false;
  }

  logout(): void {
    this.storageService.deleteUserStorage();
    this.router.navigate(["/login"])
  }
} 