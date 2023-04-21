import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ImpressionsRoutingModule } from './impressions-routing.module';
import { ImpressionsComponent } from './impressions.component';


@NgModule({
  declarations: [
    ImpressionsComponent
  ],
  imports: [
    CommonModule,
    ImpressionsRoutingModule
  ]
})
export class ImpressionsModule { }
