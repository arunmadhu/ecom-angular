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
  orderItems: any[];
  orderNumber: string;

  constructor(private user: UserService, private dataService: DataService, private router: Router) {

    this.user.currentUser.subscribe(us => this.loggedInUser = us);

    if (this.loggedInUser == '') {
      this.router.navigateByUrl(`/login`).then((e) => {
      });
    } else {

      this.dataService.get_order(this.loggedInUser).subscribe((res: any[]) => {
        console.log(res);

        this.orderItems = [
          { cartid: 1, productid: 1, productname: 'dell inspirion laptop', quatity: 2, price: 3000 },
          { cartid: 1, productid: 1, productname: 'Hp Pavilio laptop', quatity: 1, price: 1200 },
          { cartid: 1, productid: 1, productname: 'Mac air', quatity: 1, price: 2100 },
          { cartid: 1, productid: 1, productname: 'OnePlus mobile', quatity: 3, price: 3600 }
        ];
      });
    }
  }

  ngOnInit() {
  }

  public placeOrder() {

    var model = new Order();
    model.totalPrice = 3500;

    this.flashMessage = "Updating your cart..";

    this.dataService.place_order(model).subscribe(
      (val) => {
        console.log("POST call successful value returned in body", val);
        this.flashMessage = "Order submitted.";

        this.orderNumber = JSON.parse(JSON.stringify(val)).orderNumber;
      },
      response => {
        console.log("POST call in error", response);
        this.flashMessage = "Not able to update cart now. Please try later..";
      },
      () => {
        console.log("The POST observable is now completed.");
      }
    );

  }
}
