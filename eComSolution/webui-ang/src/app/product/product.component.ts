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
      console.log(res);

      this.products = [
        { id: 1, name: 'dell inspirion laptop', description: 'dell laptop with SSD', unitPrice: 1500 },
        { id: 2, name: 'Mi Poco F1', description: 'xiomi latest phone', unitPrice: 150 },
        { id: 3, name: 'Hp Pavilio laptop', description: 'Hp with SSD', unitPrice: 1200 },
        { id: 4, name: 'iphone X', description: 'latest iphone in market', unitPrice: 1900 },
        { id: 5, name: 'Mac air', description: 'apple laptop thin', unitPrice: 2100 },
        { id: 6, name: 'OnePlus mobile', description: 'google android mobile', unitPrice: 1200 }
      ];
    });
  }

  ngOnInit() {
    this.user.currentUser.subscribe(us => this.loggedInUser = us);
  }

  public addToCart(productid: number) {

    if (this.loggedInUser == '') {
      this.router.navigateByUrl(`/login`).then((e) => {
      });
    } else {
      console.log(this.loggedInUser + ' added the product: ' + productid);

      var model = new Cart();
      model.productId = productid;
      model.userEmail = this.loggedInUser;

      this.flashMessage = "Updating your cart..";

      this.dataService.update_cart(model).subscribe(
        (val) => {
          console.log("POST call successful value returned in body", JSON.parse(JSON.stringify(val)));
          this.flashMessage = "Cart updated..";
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
}
