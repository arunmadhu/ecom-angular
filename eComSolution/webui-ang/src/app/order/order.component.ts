import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { Router } from "@angular/router";
import { DataService } from 'src/app/services/data.service';
import { Order } from 'src/app/models/product';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  loggedInUser: string;
  flashMessage: string;
  newOrder: any[];
  orderCreated: boolean;

  constructor(private user: UserService, private dataService: DataService, private router: Router) {

    this.user.currentUser.subscribe(us => this.loggedInUser = us);
    this.orderCreated = false;

    if (this.loggedInUser == '') {
      this.router.navigateByUrl(`/login`).then((e) => {
      });
    } else {
      this.dataService.prepare_order(this.loggedInUser).subscribe((res: any[]) => {
        console.log(res);
        this.newOrder = res;
      });
    }
  }

  ngOnInit() {
  }

  public placeOrder() {

   var data = new Order();
    data.userId = this.loggedInUser;

    this.dataService.place_order(this.newOrder).subscribe(
      (val) => {
        this.flashMessage = "Order submitted."
        this.orderCreated = true;
      },
      response => {
        console.log("POST call in error", response);
        this.orderCreated = true;
        this.flashMessage = "Not able to update cart now. Please try later..";
      },
      () => {
      }
    );

  }
}
