import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { Basket, IBasket, IBasketItem } from '../shared/models/basket';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  baseUrl = 'https://localhost:5001/api/';
  basket: IBasket;
  private total = new BehaviorSubject<number | null>(null);
  total$ = this.total.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  getCurrentBasketValue(): IBasket {
    return JSON.parse(localStorage.getItem('basket') || '{}');
  }

  private createBasket(): IBasket {
    var basket = new Basket();
    localStorage.setItem('basket', JSON.stringify(basket));

    return basket;
  }

  addItemToBasket(item: IBasketItem) {

    if (localStorage.getItem('basket') === null) {
      this.basket = this.createBasket();
    } else {
      this.basket = JSON.parse(localStorage.getItem('basket') || '{}');
    }

    if (this.basket.items) {
      this.basket.items.push(item);
    } else {
      this.basket.items = [item];
    }

    this.basket.total = this.basket.items
      .map((item: any) => item.orderPoints)
      .reduce((prev: any, next: any) => prev + next);

      this.total.next(this.basket.total);

    localStorage.setItem('basket', JSON.stringify(this.basket));
  }

  removeItemFromBasket(itemId: string): IBasket {
    this.basket = this.getCurrentBasketValue();

    this.basket.items = this.basket.items.filter((i) => i.id !== itemId);

    if (this.basket.items.length < 1) {
      this.basket.total = 0;
    } else {
      this.basket.total = this.basket.items
        .map((item) => item.orderPoints)
        .reduce((prev, next) => prev + next);
    }

    this.total.next(this.basket.total);

    localStorage.setItem('basket', JSON.stringify(this.basket));

    return this.basket;
  }

  deleteBasket(){
    localStorage.removeItem('basket');
    
  }


}
