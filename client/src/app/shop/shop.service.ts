import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IProduct } from '../shared/models/product';
import {map} from 'rxjs/operators'; 
@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';
  bcTitle:string;

  constructor(private http: HttpClient) {}

  setBreadCrumbTitle(title: string){
    this.bcTitle=title;
  }

  // getProducts() {
  //   return this.http.get<any>(this.baseUrl + 'products');
  // }

  getNumberOfPoints() {
    return this.http.get<any>(this.baseUrl + 'products');
  }
}