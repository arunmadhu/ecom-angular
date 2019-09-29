import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { DataService } from 'src/app/services/data.service';
import { UserService } from 'src/app/services/user.service';
import { Order } from '../models/product';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  loggedInUser: string;
  flashMessage: string;
  cartItems: any[];

  constructor(private user: UserService, private dataService: DataService, private router: Router) {

    this.user.currentUser.subscribe(us => this.loggedInUser = us);

    if (this.loggedInUser == '') {
      this.router.navigateByUrl(`/login`).then((e) => {
      });
    } else {
      this.dataService.get_cart(this.loggedInUser).subscribe((res: any[]) => {
        this.cartItems = res;
      });
    }
  }

  ngOnInit() {
  }

  public removeItem(cartid) {

    this.dataService.remove_cartItem(cartid);

    let index = this.cartItems.findIndex(d => d.cartid === cartid);
    this.cartItems.splice(index, 1);
  }

  public checkOut() {
    this.router.navigateByUrl(`/orders`).then((e) => {
    });
  }
}
