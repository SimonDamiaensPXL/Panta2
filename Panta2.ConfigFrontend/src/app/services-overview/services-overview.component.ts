import { Component } from '@angular/core';
import { Service } from '../core/models/service.model';
import { ServiceService } from '../core/services/service/service.service';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-services-overview',
  templateUrl: './services-overview.component.html',
  styleUrls: ['./services-overview.component.sass']
})
export class ServicesOverviewComponent {
  services: Service[] = [];
  isLoading: boolean = true;

  constructor(private serviceService: ServiceService) { }

  async ngOnInit(): Promise<void> {
    this.services = await firstValueFrom(this.serviceService.getServices());
  }
}
