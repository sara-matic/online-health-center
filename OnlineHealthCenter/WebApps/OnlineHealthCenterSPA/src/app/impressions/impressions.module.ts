import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ImpressionsRoutingModule } from './impressions-routing.module';
import { ImpressionsComponent } from './impressions.component';
import { AddImpressionFormComponent } from './feature-add-impressions/add-impression-form/add-impression-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ShowImpressionsFormComponent } from './feature-show-impressions/show-impressions-form/show-impressions-form.component';


@NgModule({
  declarations: [
    ImpressionsComponent,
    AddImpressionFormComponent,
    ShowImpressionsFormComponent
  ],
  imports: [
    CommonModule,
    ImpressionsRoutingModule,
    ReactiveFormsModule
  ]
})
export class ImpressionsModule { }
