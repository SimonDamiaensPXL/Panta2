import { Component } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Service } from '../../core/models/service.model';
import { ServiceService } from '../../core/services/service/service.service';

@Component({
  selector: 'app-services-list',
  templateUrl: './services-list.component.html',
})
export class ServicesListComponent {
  services: Service[] = [];
  isLoading: boolean = true;

  constructor(private serviceService: ServiceService) { }

  async ngOnInit(): Promise<void> {
    this.services = await firstValueFrom(this.serviceService.getServices());
    this.isLoading = false;
  }
}
