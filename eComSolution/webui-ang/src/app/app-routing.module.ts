import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from 'src/app/home/home.component';
import { AboutComponent } from 'src/app/about/about.component';
import { CartComponent } from './cart/cart.component';
import { ProductComponent } from './product/product.component';
import { OrderComponent } from './order/order.component';
import { LoginComponent } from './login/login.component';
import { OrderListComponent } from './order/order-list.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' }, 
  { path: 'home', component: HomeComponent },
  { path: 'about', component: AboutComponent },
  { path: 'products', component: ProductComponent },
  { path: 'orders', component: OrderComponent },
  { path: 'orders-list', component: OrderListComponent },
  { path: 'cart', component: CartComponent },
  { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
