import { Component, Input } from '@angular/core';
import { Service } from '../../core/models/service.model';

@Component({
  selector: 'app-service',
  templateUrl: './service-entity.component.html',
})
export class ServiceComponent {
  @Input() service?: Service
}
