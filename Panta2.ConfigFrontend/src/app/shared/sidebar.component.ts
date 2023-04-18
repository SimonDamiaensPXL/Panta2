import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-layout-sidebar',
  templateUrl: './sidebar.component.html'
})


export class SidebarComponent implements OnInit {
  @Input() item: number = 1;
  @Output() changePage = new EventEmitter<any>();

  constructor() {}

  async ngOnInit(): Promise<void> {
  }

  changeItem(item: number): void {
    this.item = item;
    this.changePage.emit(this.item);
  }
}