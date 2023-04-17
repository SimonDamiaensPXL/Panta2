import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../core/services/user/user.service';

@Component({
  selector: 'app-layout-sidebar',
  templateUrl: './sidebar.component.html'
})


export class SidebarComponent implements OnInit {
  @Input() isSearchbarEnabled: boolean = false;
  @Input() list: any[] = []
  @Output() filtered = new EventEmitter<any[]>();

  userName?: string;
  companyLogo?: string;
  filteredItems: any[] = [];
  searchQuery: string = ""

  constructor(private userService: UserService, private router: Router) {}

  async ngOnInit(): Promise<void> {
    this.companyLogo = "../../assets/images/logos/logo_cegeka.png"
  }

  onFilter(query: string) {
    this.filteredItems = this.list.filter(item => (item.name as string).toLowerCase().includes(query.toLowerCase()));
    this.filtered.emit(this.filteredItems);
  }

  goToHome(): void {
    this.router.navigate(["/home"]);
  }
}