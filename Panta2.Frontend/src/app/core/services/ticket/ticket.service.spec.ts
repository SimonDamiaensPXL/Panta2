import { TestBed } from '@angular/core/testing';
import { HttpClientModule } from '@angular/common/http';
import { ApiService } from '../api/api.service';
import { TicketService } from './ticket.service';

describe('TicketService', () => {
  let ticketService: TicketService;
  let apiServiceMock: Partial<ApiService>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule]
    });
    ticketService = TestBed.inject(TicketService);

    apiServiceMock = {
      put: jest.fn(),
      get: jest.fn(),
    }

    ticketService = new TicketService(apiServiceMock as ApiService);
  });

});
