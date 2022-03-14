import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from './card/card.component';
import {MatFormFieldModule} from '@angular/material/form-field';
import {BsDropdownModule} from 'ngx-bootstrap/dropdown';
import {CdkStepperModule} from '@angular/cdk/stepper';
import { StepperComponent } from './components/stepper/stepper.component';




@NgModule({
  declarations: [
    // CardComponent
  
    StepperComponent
  ],
  imports: [
    CommonModule,
    MatFormFieldModule,
    BsDropdownModule.forRoot(),
    CdkStepperModule
  ],
  exports:[
    BsDropdownModule,
    CdkStepperModule,
    StepperComponent
  ]
})
export class SharedModule { }
