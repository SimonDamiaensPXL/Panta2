import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { StorageService } from '../core/services/storage/storage.service';

@Component({
  selector: 'app-layout-sidebar',
  templateUrl: './sidebar.component.html'
})


export class SidebarComponent implements OnInit {
  item?: string;

  constructor(private storageService: StorageService, private router: Router) {}

  async ngOnInit(): Promise<void> {
    this.item = await this.storageService.getItem();
  }

  changeItem(item: string): void {
    this.item = item;
    this.storageService.changeItem(this.item);
    this.router.navigate([`/${item}`]);
  }
}