import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { HomeComponent  } from 'src/app/home/home.component';
import { AboutComponent } from './about/about.component';

import { UserService  } from 'src/app/services/user.service';
import { CartComponent } from './cart/cart.component';
import { ProductComponent } from './product/product.component';
import { OrderComponent } from './order/order.component';
import { LoginComponent } from './login/login.component';
import { OrderListComponent  } from './order/order-list.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AboutComponent,
    CartComponent,
    ProductComponent,
    OrderComponent,
    LoginComponent,
    OrderListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
