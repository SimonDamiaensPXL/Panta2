import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { Service } from '../core/models/service.model';
import { User } from '../core/models/user.model';
import { ServiceService } from '../core/services/service/service.service';
import { StorageService } from '../core/services/storage/storage.service';
import { UserService } from '../core/services/user/user.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.sass']
})
export class SettingsComponent implements OnInit {
  services: Service[] = [];
  isLoading: boolean = true;
  isError: boolean = false;
  errorMessage?: string;
  name?: string;
  inputName?: string;

  constructor(private storageService: StorageService, private serviceService: ServiceService, private userService: UserService) {}

  async ngOnInit(): Promise<void> {
    try {
      const user: User = await this.storageService.getUser();
      this.name = user.firstName;
      this.inputName = user.firstName;
      this.services = await firstValueFrom(this.userService.getIsFavoriteServices(user.id));
      this.isLoading = false;
    } catch(error) {
      console.error(error);
    }
  }

  async editFavorite(serviceId: number, isFavorite: boolean): Promise<void> {
    const user: User = await this.storageService.getUser();
    this.userService.editFavorite(user.id, serviceId, isFavorite).subscribe({
      next() {
        window.location.reload();
      },
      error: (err) => {
        console.error(err);
        this.errorMessage = "Sorry, you cannot add more than 5 favorites. Please remove an existing favorite to add a new one."
        this.isError = true;
      },
    })
  }

  async changeName() : Promise<void> {
    console.log(this.inputName);
    if (!this.inputName) {
      this.errorMessage = "Please make sure you have entered a valid username and try again."
      this.isError = true
      return;
    }

    const user: User = await this.storageService.getUser();
    this.userService.changeName(this.inputName, user.id).subscribe({
      next() {
        window.location.reload();
      },
      error: (err) => {
        this.errorMessage = "We're sorry, there was a problem updating your username. Please make sure you have entered a valid username and try again."
        this.isError = true
      }
    })
  }

  closeFavoriteError(): void {
    this.isError = false;
    window.location.reload();
  }
}
