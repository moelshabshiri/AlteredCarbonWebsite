import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IOrderReturn } from 'src/app/shared/models/order';
import { OrdersService } from '../orders.service';

@Component({
  selector: 'app-order-detailed',
  templateUrl: './order-detailed.component.html',
  styleUrls: ['./order-detailed.component.scss'],
})
export class OrderDetailedComponent implements OnInit {
  order: IOrderReturn;
  checked: boolean[];

  constructor(
    private route: ActivatedRoute,
    private orderService: OrdersService
  ) {}

  ngOnInit(): void {

    this.orderService
      .getOrder(+this.route.snapshot.paramMap.get('id')!)
      .subscribe(
        (data) => {
         
          this.order = data;
          this.checked = new Array(this.order.orderHistories.length).fill(
            false
          );
        },
        (error) => {
        }
      );
  }

  getOrder() {}

  clicked(i: number) {
    this.checked[i] = !this.checked[i];
  }
}
