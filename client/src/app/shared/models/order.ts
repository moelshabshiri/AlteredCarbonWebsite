import { IAddress } from './address';

export interface IOrder {
  items: IOrderItem[];
}

export interface IOrderItem {
  type: string;
  typeValue: number;
  acres: number;
}

export interface IOrderReturn {
  id: number;
  farmerEmail: string;
  approvedBy: string;
  datetimeOfOrderCreation: Date;
  datetimeOfOrderApproval: Date;
  status: string;
  orderPoints: number;
  items: IOrderItem[];
  orderHistories:any;
}

export interface IHis {
  id: number;
  farmerEmail: string;
  approvedBy: string;
  datetimeOfOrderCreation: Date;
  datetimeOfOrderApproval: Date;
  status: string;
  orderPoints: number;
  items: IOrderItem[];
  orderHistories:any;
}
