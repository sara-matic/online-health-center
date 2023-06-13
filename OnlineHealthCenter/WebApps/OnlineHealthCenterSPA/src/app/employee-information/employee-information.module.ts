import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeeInformationRoutingModule } from './employee-information-routing.module';
import { EmployeeInformationComponent } from './employee-information.component';
import { EmployeeInformationFormComponent } from './feature-employee-information/employee-information-form/employee-information-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ClinicInformationComponent } from './feature-clinic-information/clinic-information/clinic-information.component';
import { AddDoctorFormComponent } from './feature-add-doctor/add-doctor-form/add-doctor-form.component';
import { UpdateDoctorFormComponent } from './feature-update-doctor/update-doctor-form/update-doctor-form.component';


@NgModule({
  declarations: [
    EmployeeInformationComponent,
    EmployeeInformationFormComponent,
    ClinicInformationComponent,
    AddDoctorFormComponent,
    UpdateDoctorFormComponent
  ],
  imports: [
    CommonModule,
    EmployeeInformationRoutingModule,
    ReactiveFormsModule
  ]
})
export class EmployeeInformationModule { }
