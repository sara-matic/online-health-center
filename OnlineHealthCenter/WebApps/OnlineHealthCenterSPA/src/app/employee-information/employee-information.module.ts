import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeeInformationRoutingModule } from './employee-information-routing.module';
import { EmployeeInformationComponent } from './employee-information.component';


@NgModule({
  declarations: [
    EmployeeInformationComponent
  ],
  imports: [
    CommonModule,
    EmployeeInformationRoutingModule
  ]
})
export class EmployeeInformationModule { }
