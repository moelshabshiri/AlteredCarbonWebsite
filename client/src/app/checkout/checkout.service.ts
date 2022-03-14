import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IOrder } from '../shared/models/order';

@Injectable({
  providedIn: 'root',
})
export class CheckoutService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) {}

  createOrder(order: IOrder) {
    return this.http.post(this.baseUrl + 'orders', order);
  }
}
