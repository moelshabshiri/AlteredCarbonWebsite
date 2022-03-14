import { v4 as uuidv4 } from 'uuid';

// export interface IOrder {
//   id: string;
//   items: IBasketItem[];
//   total:number
// }

export interface IBasket {
  id: string;
  total: number;
  items: IBasketItem[];
}

export interface IBasketItem {
  id: string;
  type: string;
  value: number;
  acres: number;
  pop: number;
  orderPoints: number;
}

export class Basket implements IBasket {
  id: string;
  total: number=0;
  items: IBasketItem[];
}


