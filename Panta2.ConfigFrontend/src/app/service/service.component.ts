import { Component, Input } from '@angular/core';
import { Service } from '../core/models/service.model';

@Component({
  selector: 'app-service',
  templateUrl: './service.component.html',
  styleUrls: ['./service.component.sass']
})
export class ServiceComponent {
  @Input() service?: Service
}
