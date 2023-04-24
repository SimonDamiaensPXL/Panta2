import { Component, Input } from '@angular/core';
import { Company } from '../core/models/company.model';

@Component({
  selector: 'app-company',
  templateUrl: './company-entity.component.html',
})
export class CompanyComponent {
  @Input() company?: Company
}
