import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppointmentsComponent } from './appointments.component';
import { ScheduleFormComponent } from './feature-show-schedule/schedule-form/schedule-form.component';
import { CreateCouponFormComponent } from './feature-show-schedule/create-coupon-form/create-coupon-form/create-coupon-form.component';

const routes: Routes = [ {path: 'request-appointments', component: AppointmentsComponent},
  {path: 'schedule', component: ScheduleFormComponent, children: [{path: '', component: CreateCouponFormComponent}]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppointmentsRoutingModule { }
