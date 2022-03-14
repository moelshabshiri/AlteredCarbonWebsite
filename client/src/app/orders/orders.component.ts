import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from '../account/account.service';
import { IOrderReturn } from '../shared/models/order';
import { IUser } from '../shared/models/user';
import { OrdersService } from './orders.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss'],
})
export class OrdersComponent implements OnInit {
  orders: IOrderReturn[];
  currentUser$: Observable<IUser | null>;
  currentAcountType: string | null;
  constructor(
    private orderService: OrdersService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
    this.currentUser$.subscribe((x) => {
      if (x?.accountType == 'cooperative') {
        this.getAllOrders();
      } else {
        this.getOrderByUser();
      }
    });
  }

  getOrderByUser() {
    this.orderService.getOrdersByUser().subscribe(
      (data) => {
        this.orders = data;
      },
      (error) => {
      }
    );
  }

  getAllOrders() {
    this.orderService.getAllOrders().subscribe(
      (data) => {
        this.orders = data;
      },
      (error) => {
      }
    );
  }
}
