import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutComponent } from './checkout.component';
import { CheckoutRoutingModule } from './checkout-routing.module';
import { SharedModule } from '../shared/shared.module';
import { CheckoutSuccessComponent } from './checkout-success/checkout-success.component';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    CheckoutComponent,
    CheckoutSuccessComponent,
    
  ],
  imports: [
    CommonModule,
    CheckoutRoutingModule,
    SharedModule,
    ReactiveFormsModule
  ], 
  
})
export class CheckoutModule { }
