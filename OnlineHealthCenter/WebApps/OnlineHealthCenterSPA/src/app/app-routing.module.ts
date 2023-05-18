import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [{ path: 'appointments', loadChildren: () => import('./appointments/appointments.module').then(m => m.AppointmentsModule) },
 { path: 'impressions', loadChildren: () => import('./impressions/impressions.module').then(m => m.ImpressionsModule) },
 { path: 'start-page', loadChildren: () => import('./employee-information/employee-information.module').then(m => m.EmployeeInformationModule) }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }