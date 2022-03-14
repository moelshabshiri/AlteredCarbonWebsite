import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { IOrderItem, IOrderReturn } from 'src/app/shared/models/order';
import { IUser } from 'src/app/shared/models/user';
import { OrdersService } from '../orders.service';

@Component({
  selector: 'app-item-detailed',
  templateUrl: './item-detailed.component.html',
  styleUrls: ['./item-detailed.component.scss'],
})
export class ItemDetailedComponent implements OnInit {
  // order: IOrderReturn;
  orderHistory: any;
  comment: string;
  @Input() orderId: any;
  @Input() historyId: any;
  @Input() historyIteration: any;
  @Input() orderLength: any;
  currentUser$: Observable<IUser | null>;

  constructor(
    private route: ActivatedRoute,
    private orderService: OrdersService,
    public toastr: ToastrService,
    private accountService: AccountService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;

    this.orderService.getHistory(this.historyId).subscribe(
      (data) => {
        this.orderHistory = data;
      },
      (error) => {
      }
    );
  }

  acceptOrder() {
    this.orderService.acceptOrder(this.orderId, this.orderHistory.id).subscribe(
      (order: any) => {
        this.toastr.success("Order Accepted");
      },
      (error) => {
        this.toastr.error(error.message);
      }
    );
  }

  commentOrder(acceptedItems: number[], pendingItems: number[]) {
    console.log(this.comment);
    this.orderService
      .commentOrder(
        this.orderId,
        this.orderHistory.id,
        this.comment,
        acceptedItems,
        pendingItems
      )
      .subscribe(
        (order: any) => {
          this.toastr.success("Order Commented");
        },
        (error) => {
          this.toastr.error(error.message);
        }
      );
  }

  submitChanges() {
    const items = this.orderHistory.orderItems.filter(
      (x: any) => x.approved == false
    );

    //accept order

    if (
      items.length == 0 &&
      (this.orderHistory.status == 'pending' ||
        this.orderHistory.status == 'farmerReviewed')
    ) {
      this.acceptOrder();
    }

    //comment on order
    else if (
      items.length > 0 &&
      (this.orderHistory.status == 'pending' ||
        this.orderHistory.status == 'farmerReviewed')
    ) {
      const acceptedItems = this.orderHistory.orderItems.filter(
        (x: any) => x.approved == true
      );

      this.commentOrder(
        acceptedItems.map((a: any) => a.id),
        items.map((a: any) => a.id)
      );
    }
    items.forEach((x: any) => delete x.id);
   
    this.router.navigateByUrl('/');
  }

  update() {
    const items = this.orderHistory.orderItems.filter(
      (x: any) => x.approved == false
    );

    items.forEach((x: any) => delete x.id);

    this.orderService
      .updateOrder(this.orderId, this.orderHistory.id, items)
      .subscribe(
        (order: any) => {},
        (error) => {
          this.toastr.error(error.message);
        }
      );

    this.router.navigateByUrl('/');
  }
}
