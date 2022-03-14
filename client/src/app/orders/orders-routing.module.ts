import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderDetailedComponent } from './order-detailed/order-detailed.component';
import { RouterModule, Routes } from '@angular/router';
import { OrdersComponent } from './orders.component';
import { ItemDetailedComponent } from './item-detailed/item-detailed.component';

const routes: Routes = [
  { path: '', component: OrdersComponent },
  {
    path: ':id',
    component: OrderDetailedComponent,
    data: { breadcrumb: { alias: 'OrderDetailed' } },
  },
  {
    path: 'history/:id',
    component: ItemDetailedComponent,
    data: { breadcrumb: { alias: 'ItemsDetails' } },
  },
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class OrdersRoutingModule {}
