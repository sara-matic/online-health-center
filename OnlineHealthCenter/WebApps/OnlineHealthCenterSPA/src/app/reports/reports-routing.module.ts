import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ReportsComponent } from './reports.component';
import { CreateReportFormComponent } from './feature-create-report/create-report-form/create-report-form.component';
import { ShowReportsFormComponent } from './feature-show-reports/show-reports-form/show-reports-form.component';

const routes: Routes = [
  { path: '', component: ReportsComponent },
  { path: 'create', component: CreateReportFormComponent },
  { path: 'showAll', component: ShowReportsFormComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportsRoutingModule { }
