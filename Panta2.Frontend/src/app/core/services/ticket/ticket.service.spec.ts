import { TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { ApiService } from '../api/api.service';
import { TicketService } from './ticket.service';
import { Observable, of } from 'rxjs';

describe('TicketService', () => {
  let ticketService: TicketService;
  let apiServiceMock: Partial<ApiService>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule]
    });
    ticketService = TestBed.inject(TicketService);

    apiServiceMock = {
      get: jest.fn()
    }

    ticketService = new TicketService(apiServiceMock as ApiService);
  });

  it('should be created', () => {
    expect(ticketService).toBeTruthy();
  });


  describe('getTickets', () => {
    it('should call the apiService get method with the correct endpoint and return the result', () => {
      const mockResponse = [
        { ticketNum: 12345, subject: 'Ticket 1', priority: 'Normal', state: 'Open', creationDate: new Date(), lastModificationDate: new Date() }
      ];
      (apiServiceMock.get as jest.Mock).mockReturnValue(of(mockResponse));

      const result = ticketService.getTickets();

      expect(apiServiceMock.get).toHaveBeenCalledWith('/tickets');
      expect(result).toBeInstanceOf(Observable);
      result.subscribe((response) => {
        expect(response).toEqual(mockResponse);
      });
    });
  });

  
  describe('getTicketsCountedPerState', () => {
    it('should call the apiService get method with the correct endpoint and return the result', () => {
      const mockResponse = { openTicketCount: 4, inProgressTicketCount: 3, closedTicketCount: 3 };

      (apiServiceMock.get as jest.Mock).mockReturnValue(of(mockResponse));

      const result = ticketService.getTicketsCountedPerState();

      expect(apiServiceMock.get).toHaveBeenCalledWith('/tickets/state-counted');
      expect(result).toBeInstanceOf(Observable);
      result.subscribe((response) => {
        expect(response).toEqual(mockResponse);
      });
    });
  });

  
  describe('getTicketsCountedPerPriority', () => {
    it('should call the apiService get method with the correct endpoint and return the result', () => {
      const mockResponse = { normalTicketCount: 5, highTicketCount: 3, emergencyTicketCount: 1 };

      (apiServiceMock.get as jest.Mock).mockReturnValue(of(mockResponse));

      const result = ticketService.getTicketsCountedPerPriority();

      expect(apiServiceMock.get).toHaveBeenCalledWith('/tickets/priority-counted');
      expect(result).toBeInstanceOf(Observable);
      result.subscribe((response) => {
        expect(response).toEqual(mockResponse);
      });
    });
  });
});


