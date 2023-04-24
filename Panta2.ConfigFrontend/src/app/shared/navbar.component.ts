import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../core/services/user/user.service';
import { StorageService } from '../core/services/storage/storage.service';

@Component({
  selector: 'app-layout-navbar',
  templateUrl: './navbar.component.html'
})


export class NavbarComponent implements OnInit {
  @Input() isSearchbarEnabled: boolean = false;
  @Input() list: any[] = []
  @Output() filtered = new EventEmitter<any[]>();

  userName?: string;
  companyLogo?: string;
  filteredItems: any[] = [];
  searchQuery: string = ""

  constructor(private storageService: StorageService, private router: Router) {}

  async ngOnInit(): Promise<void> {
    this.companyLogo = "../../assets/images/logos/logo_cegeka.png"
  }

  onFilter(query: string) {
    this.filteredItems = this.list.filter(item => (item.name as string).toLowerCase().includes(query.toLowerCase()));
    this.filtered.emit(this.filteredItems);
  }

  goToHome(): void {
    this.storageService.changeItem('home');
    this.router.navigate(["/home"]);
  }
}