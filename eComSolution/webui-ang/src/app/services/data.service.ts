import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Cart, Product, Order } from 'src/app/models/product';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class DataService {
  baseUrl: string = "https://localhost:44359/api/ecom";

  constructor(private httpClient: HttpClient) {
  }

  get_products() {
    return this.httpClient.get(this.baseUrl + '/products');
  }

  get_cart(userid) {
    return this.httpClient.get(this.baseUrl + '/cart/' + userid);
  }

  get_orders(userid) {
    return this.httpClient.get(this.baseUrl + '/orders/' + userid);
  }

  prepare_order(userid) {
    return this.httpClient.get(this.baseUrl + '/prepareorder/' + userid);
  }

  update_cart(cart: Cart): Observable<number> {
    return this.httpClient.post<number>(this.baseUrl + '/cart', cart);
  }

  place_order(data: any[]): Observable<string> {
    return this.httpClient.post<string>(this.baseUrl + '/order', data);
  }

  remove_cartItem(id: number) {
    console.log(`${this.baseUrl}/deletecart/${id}`);
    return this.httpClient.delete<void>(`${this.baseUrl}/deletecart/${id}`);
  }
}

