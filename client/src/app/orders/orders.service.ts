import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map, Observable } from 'rxjs';
import { IOrder, IOrderReturn } from '../shared/models/order';

@Injectable({
  providedIn: 'root',
})
export class OrdersService {
  baseUrl = 'https://localhost:5001/api/';
  order: IOrderReturn[] = [];
  constructor(private http: HttpClient, private router: Router) {}


  getOrdersByUser(): Observable<IOrderReturn[]> {
    return this.http.get<IOrderReturn[]>('https://localhost:5001/api/orders/');
  }

  getAllOrders(): Observable<IOrderReturn[]> {
    return this.http.get<IOrderReturn[]>(
      'https://localhost:5001/api/orders/all'
    );
  }

  getOrder(id: number): Observable<IOrderReturn> {
    return this.http.get<IOrderReturn>(
      `https://localhost:5001/api/orders/${id}`
    );
  }

  getHistory(id: number): Observable<IOrderReturn> {
    return this.http.get<IOrderReturn>(
      `https://localhost:5001/api/orders/history/${id}`
    );
  }

  updateOrder(
    orderId: number,
    historyId: number,
    items: any[]
  ): Observable<IOrderReturn> {
    return this.http.put<IOrderReturn>(
      `https://localhost:5001/api/orders/update`,
      { OrderId: orderId, HistoryId: historyId, Items: items }
    );
  }



  acceptOrder(
    orderId: number,
    historyId: number,
  ): Observable<IOrderReturn> {
    return this.http.put<IOrderReturn>(
      `https://localhost:5001/api/orders/accept`, 
      { OrderId: orderId, HistoryId: historyId }
    );
  }


  commentOrder(
    orderId: number,
    historyId: number,
    comment:string,
    aceeptedItemsId: number[],
    pendingItemsId: number[]
  ): Observable<IOrderReturn> {
    return this.http.put<IOrderReturn>(
      `https://localhost:5001/api/orders/comment`,
      { OrderId: orderId, HistoryId: historyId, Comment:comment, AcceptedItemsId: aceeptedItemsId, PendingItemsId: pendingItemsId }
    );

    
  }
}
