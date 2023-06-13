import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeInformationComponent } from './employee-information.component';
import { AddDoctorFormComponent } from './feature-add-doctor/add-doctor-form/add-doctor-form.component';

const routes: Routes = [{ path: '', component: EmployeeInformationComponent, children: [{path: '', component: AddDoctorFormComponent}]}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeInformationRoutingModule { }
