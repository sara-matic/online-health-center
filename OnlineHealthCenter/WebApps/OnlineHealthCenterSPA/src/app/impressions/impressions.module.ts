import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ImpressionsRoutingModule } from './impressions-routing.module';
import { ImpressionsComponent } from './impressions.component';
import { AddImpressionFormComponent } from './feature-add-impressions/add-impression-form/add-impression-form.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    ImpressionsComponent,
    AddImpressionFormComponent
  ],
  imports: [
    CommonModule,
    ImpressionsRoutingModule,
    ReactiveFormsModule
  ]
})
export class ImpressionsModule { }
