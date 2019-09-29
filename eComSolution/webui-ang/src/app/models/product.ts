export class Product {

}

export class Cart {
  userEmail: string;
  productId: number;
  price: number;
  quantity: number;
}

export class Order {
  userId: string;
  totalPrice: number;
}
