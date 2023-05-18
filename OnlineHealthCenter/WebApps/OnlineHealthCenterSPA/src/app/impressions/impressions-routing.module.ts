import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ImpressionsComponent } from './impressions.component';
import { AddImpressionFormComponent } from './feature-add-impressions/add-impression-form/add-impression-form.component';

const routes: Routes = [{ path: 'leave-impression', component: ImpressionsComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ImpressionsRoutingModule { }
