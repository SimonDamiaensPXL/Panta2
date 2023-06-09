import { Component, OnInit } from "@angular/core";
import { StorageService } from "../core/services/storage/storage.service";
import { UserService } from "../core/services/user/user.service";
import { User } from "../core/models/user.model";
import { firstValueFrom } from 'rxjs';
import { Service } from "../core/models/service.model";
import { Ticket } from "../core/models/ticket.model";
import { TicketService } from "../core/services/ticket/ticket.service";
import { formatDate } from "@angular/common";

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {
  tickets: Ticket[] = [];
  countedPerStateTickets: any;
  countedPerPriorityTickets: any;
  services: Service[] = [];
  favoriteServices: Service[] = [];
  filteredServices: Service[] = [];
  userName?: string;
  companyLogo?: string;
  isLoading: boolean = true;

  constructor(private storageService: StorageService, private userService: UserService, private ticketService: TicketService) { }

  async ngOnInit(): Promise<void> {
    try {
      const user: User = await this.storageService.getUser();

      this.countedPerStateTickets = await firstValueFrom(this.ticketService.getTicketsCountedPerState());
      this.countedPerPriorityTickets = await firstValueFrom(this.ticketService.getTicketsCountedPerPriority());
      this.tickets = await firstValueFrom(this.ticketService.getTickets());
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
  }

  getDateFormatted(date: Date): string {
    return formatDate(date, 'dd-MM-yyyy', 'en-US');
  }
} 