import { Component, Input } from '@angular/core';
import { Company } from '../core/models/company.model';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.sass']
})
export class CompanyComponent {
  @Input() company?: Company
}
