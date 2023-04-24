import { Component } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Service } from '../core/models/service.model';
import { ServiceService } from '../core/services/service/service.service';

@Component({
  selector: 'app-service-list',
  templateUrl: './service-list.component.html',
})
export class ServiceListComponent {
  services: Service[] = [];
  isLoading: boolean = true;

  constructor(private serviceService: ServiceService) { }

  async ngOnInit(): Promise<void> {
    this.services = await firstValueFrom(this.serviceService.getServices());
    this.isLoading = false;
  }
}
