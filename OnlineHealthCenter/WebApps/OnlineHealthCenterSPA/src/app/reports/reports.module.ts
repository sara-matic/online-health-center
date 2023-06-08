import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';

import { ReportsRoutingModule } from './reports-routing.module';
import { ReportsComponent } from './reports.component';
import { CreateReportFormComponent } from './feature-create-report/create-report-form/create-report-form.component';
import { ShowReportsFormComponent } from './feature-show-reports/show-reports-form/show-reports-form.component';

@NgModule({
  declarations: [
    ReportsComponent,
    CreateReportFormComponent,
    ShowReportsFormComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    ReportsRoutingModule,
    FormsModule,
  ]
})
export class ReportsModule { }
