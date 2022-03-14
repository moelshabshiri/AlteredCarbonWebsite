import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  form: FormGroup;
  constructor(
    private accountService: AccountService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.createForm();
  }

  createForm() {
    this.form = new FormGroup({
      name: new FormControl('', Validators.required),
      email: new FormControl('', [
        Validators.required,
        Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$'),
      ]),
      password: new FormControl('', [
        Validators.required,
        Validators.pattern("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$"),
      ]),
      confirmPassword: new FormControl('', [
        Validators.required,
      ]),
      phoneNumber: new FormControl('', Validators.required),
      type: new FormControl('', Validators.required),
    });
  }

  onSubmit(invalid: boolean) {

    if (invalid) {
      this.toastr.error('Some inputs are invalid');
    } else {
      this.accountService
        .register(
          {
            name: this.form.get('name')?.value,
            email: this.form.get('email')?.value,
            password: this.form.get('password')?.value,
            phoneNumber: this.form.get('phoneNumber')?.value,
          },
          this.form.get('type')?.value
        )
        .subscribe(
          () => {
            this.router.navigateByUrl('/');
          },
          (error) => {
          }
        );
    }
  }
}
