import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderDetailedComponent } from './order-detailed/order-detailed.component';
import { OrdersComponent } from './orders.component';
import { OrdersRoutingModule } from './orders-routing.module';
import { ItemDetailedComponent } from './item-detailed/item-detailed.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [OrderDetailedComponent, OrdersComponent, ItemDetailedComponent],
  imports: [CommonModule, OrdersRoutingModule, FormsModule],
  exports: [OrderDetailedComponent, OrdersComponent],
})
export class OrdersModule {}
