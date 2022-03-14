import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { ShopComponent } from '../shop/shop.component';
import { ShopModule } from '../shop/shop.module';



@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    CommonModule,
    ShopModule
  ],
})
export class HomeModule { }
