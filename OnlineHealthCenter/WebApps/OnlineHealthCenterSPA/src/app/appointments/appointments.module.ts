import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AppointmentsRoutingModule } from './appointments-routing.module';
import { AppointmentsComponent } from './appointments.component';
import { RequestFormComponent } from './feature-appointments-scheduleing/appointments-request-form/request-form/request-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ScheduleFormComponent } from './feature-show-schedule/schedule-form/schedule-form.component';
import { CreateCouponFormComponent } from './feature-show-schedule/create-coupon-form/create-coupon-form/create-coupon-form.component';

@NgModule({
  declarations: [
    AppointmentsComponent,
    RequestFormComponent,
    ScheduleFormComponent,
    CreateCouponFormComponent
  ],
  imports: [
    CommonModule,
    AppointmentsRoutingModule,
    ReactiveFormsModule
    ]
})

export class AppointmentsModule { }
