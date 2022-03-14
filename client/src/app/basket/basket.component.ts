import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket.service';
import { Basket, IBasket, IBasketItem } from '../shared/models/basket';
import { IOrder, IOrderItem } from '../shared/models/order';
import { CheckoutService } from '../checkout/checkout.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss'],
})
export class BasketComponent implements OnInit {
  basket: IBasket;

  constructor(private basketService: BasketService, private checkoutService: CheckoutService, private router: Router, private toastr: ToastrService ) {}

  ngOnInit(): void {
    this.basket = this.basketService.getCurrentBasketValue();
  }
  
  

  removeBasketItem(id: string) {
    this.basket =  this.basketService.removeItemFromBasket(id);
  }

  
  submitOrder() {
    let items: IOrderItem[] = [];
    const basket = this.basketService.getCurrentBasketValue();
    basket.items.forEach((item) => {
      items.push({
        type: item.type,
        typeValue: item.value,
        acres: item.acres,
      });
    });

    let order: IOrder = {
      items: items,
    };

    this.checkoutService.createOrder(order).subscribe(
      (order: any) => {
        this.basketService.deleteBasket();
        this.toastr.success('Order Created successfully');
        this.router.navigate(['checkout/success']);
      },
      (error) => {
        this.toastr.error(error.message);
      }
    );
  }
  
}
