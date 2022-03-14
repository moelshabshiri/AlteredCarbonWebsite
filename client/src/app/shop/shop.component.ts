import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  @ViewChild('search', {static:true}) searchTerm: ElementRef;
  products: IProduct[] = [];
  search:string;


  constructor(private shopService: ShopService) {}

  ngOnInit(): void {
  }

 



  onReset(){
    this.searchTerm.nativeElement.value='';
    this.search='';
  }
}
