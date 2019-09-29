import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { Router } from "@angular/router";
import { DataService } from 'src/app/services/data.service';
import { Cart, Product } from 'src/app/models/product';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {
  loggedInUser: string;
  products: any[];
  flashMessage: string;

  constructor(private user: UserService, private dataService: DataService, private router: Router) {

    this.dataService.get_products().subscribe((res: any[]) => {
      this.products = res;
    });
  }

  ngOnInit() {
    this.user.currentUser.subscribe(us => this.loggedInUser = us);
  }

  public addToCart(productid: number, unitprice: number) {

    if (this.loggedInUser == '') {
      this.router.navigateByUrl(`/login`).then((e) => {
      });
    } else {

      var model = new Cart();
      model.productId = productid;
      model.userEmail = this.loggedInUser;
      model.price = unitprice;
      model.quantity = 1;

      this.flashMessage = "Updating your cart..";

      this.dataService.update_cart(model).subscribe(
        (val) => {
          this.flashMessage = "Cart updated..";
        },
        response => {
          console.log("POST call in error", response);
          this.flashMessage = "Not able to update cart now. Please try later..";
        },
        () => {
        }
      );
    }
  }
}
