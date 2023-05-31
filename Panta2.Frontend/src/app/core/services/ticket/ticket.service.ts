import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from '../api/api.service';
import { Ticket } from '../../models/ticket.model';

@Injectable({
  providedIn: 'root',
})
export class TicketService {
  constructor(private apiService: ApiService) { }

  getTickets(): Observable<Ticket[]> {
    return this.apiService.get(`/tickets`);
  }

  getTicketsCountedPerState(): Observable<any> {
    return this.apiService.get(`/tickets/state-counted`);
  }
  getTicketsCountedPerPriority(): Observable<any> {
    return this.apiService.get(`/tickets/priority-counted`);
  }
}