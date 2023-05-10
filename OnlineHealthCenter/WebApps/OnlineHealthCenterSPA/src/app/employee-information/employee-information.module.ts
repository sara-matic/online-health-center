import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeeInformationRoutingModule } from './employee-information-routing.module';
import { EmployeeInformationComponent } from './employee-information.component';
import { EmployeeInformationFormComponent } from './feature-employee-information/employee-information-form/employee-information-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ClinicInformationComponent } from './feature-clinic-information/clinic-information/clinic-information.component';


@NgModule({
  declarations: [
    EmployeeInformationComponent,
    EmployeeInformationFormComponent,
    ClinicInformationComponent
  ],
  imports: [
    CommonModule,
    EmployeeInformationRoutingModule,
    ReactiveFormsModule
  ]
})
export class EmployeeInformationModule { }
