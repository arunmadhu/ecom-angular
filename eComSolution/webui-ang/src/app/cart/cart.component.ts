import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { DataService } from 'src/app/services/data.service';
import { UserService } from 'src/app/services/user.service';

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
        console.log(res);

        this.cartItems = [
          { cartid: 1, productid: 1, productname: 'dell inspirion laptop', quatity: 2, price:3000},
          { cartid: 1, productid: 1, productname: 'Hp Pavilio laptop', quatity: 1, price:1200},
          { cartid: 1, productid: 1, productname: 'Mac air', quatity: 1, price:2100},
          { cartid: 1, productid: 1, productname: 'OnePlus mobile', quatity: 3, price: 3600}
        ];
      });

    }
  }

  ngOnInit() {
  }

  public removeItem(cartid) {
    console.log(this.loggedInUser + ' removed cart item no: ' + cartid);

    let index = this.cartItems.findIndex(d => d.cartid === cartid);
    this.cartItems.splice(index, 1);

  }

  public checkOut() {
    this.router.navigateByUrl(`/orders`).then((e) => {
    });
    console.log(this.loggedInUser + 'checking out the cart');
  }
}
