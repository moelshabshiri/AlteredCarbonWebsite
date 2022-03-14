import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/account/account.service';
import { IBasketItem } from '../../shared/models/basket';
import { v4 as uuidv4 } from 'uuid';
import { BasketService } from 'src/app/basket/basket.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {
  message: string;
  type: any;

  form = new FormGroup({
    value: new FormControl('', Validators.required),
    acres: new FormControl('', Validators.required),
  });

  constructor(
    private accountService: AccountService,
    private basketService: BasketService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.message = '';
    this.type = this.route.snapshot.queryParamMap.get('type');
  }

  onSubmit() {
    let pop: number = 0;
    let CurrentOrderPoints: number = 0;

  
    CurrentOrderPoints = this.form.get('value')?.value * 24 * this.form.get('acres')?.value;

    this.message = `Total number of ${CurrentOrderPoints}`;

    const item: IBasketItem = {
      id: uuidv4(),
      type: this.type,
      value: this.form.get('value')?.value,
      acres: this.form.get('acres')?.value,
      pop: pop,
      orderPoints: CurrentOrderPoints,
    };

    this.basketService.addItemToBasket(item);

    this.router.navigateByUrl('/');

  }
}
