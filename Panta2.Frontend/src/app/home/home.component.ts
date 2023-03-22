import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { StorageService } from "../core/services/storage/storage.service";
import { UserService } from "../core/services/user/user.service";

@Component({
  selector: 'app-home-page',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {
  content?: string;

  constructor(private storageService: StorageService, private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    // if (!this.storageService.isLoggedIn()) {
    //   this.router.navigate(['/login']);
    // }
    // this.userService.getPublicContent().subscribe({
    //   next: data => {
    //     this.content = data;
    //   },
    //   error: err => {console.log(err)
    //     if (err.error) {
    //       this.content = JSON.parse(err.error).message;
    //     } else {
    //       this.content = "Error with status: " + err.status;
    //     }
    //   }
    // });
  }
}