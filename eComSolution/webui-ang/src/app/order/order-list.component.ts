import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { DataService } from 'src/app/services/data.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'order-list',
  templateUrl: './order-list.component.html'
})

export class OrderListComponent implements OnInit {
  loggedInUser: string;

   ngOnInit(): void {
    }

  constructor(private user: UserService, private dataService: DataService, private router: Router) {

    this.user.currentUser.subscribe(us => this.loggedInUser = us);

    if (this.loggedInUser == '') {
      this.router.navigateByUrl(`/login`).then((e) => {
      });
    }
  }
}
