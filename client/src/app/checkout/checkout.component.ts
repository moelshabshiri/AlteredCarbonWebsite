import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account/account.service';
import { BasketService } from '../basket/basket.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss'],
})
export class CheckoutComponent implements OnInit {
  checkoutForm: FormGroup;
  total: number;

  constructor(
    private basketService: BasketService,
    private fb: FormBuilder,
    private accountService: AccountService
  ) {
    this.createCheckoutForm();
    this.getAddressFormValues();
  }

  ngOnInit(): void {
    this.total = this.basketService.getCurrentBasketValue().total;
  }

  createCheckoutForm() {
    this.checkoutForm = this.fb.group({
      addressForm: this.fb.group({
        firstName: [null, Validators.required],
        lastName: [null, Validators.required],
        street: [null, Validators.required],
        city: [null, Validators.required],
        zipCode: [null, Validators.required],
      }),

      paymentForm: this.fb.group({
        nameOnCard: [null, Validators.required],
      }),
    });
  }

  getAddressFormValues() {
    this.accountService.getUserAddress().subscribe( 
      (address) => {
        if (address) {
          this.checkoutForm.get('addressForm')?.patchValue(address);
        }
      },
      (error) => {
      }
    );
  }
}
