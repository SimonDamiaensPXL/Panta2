import { Component } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Service } from 'src/app/core/models/service.model';
import { ServiceService } from 'src/app/core/services/service/service.service';

@Component({
  selector: 'app-services-table',
  templateUrl: './services-table.component.html'
})
export class ServicesTableComponent {
  services: Service[] = [];
  isLoading: boolean = true;

  constructor(private serviceService: ServiceService) { }

  async ngOnInit(): Promise<void> {
    this.services = await firstValueFrom(this.serviceService.getServices());
    this.isLoading = false;
  }
}
