import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { catchError, of, take } from 'rxjs';
import { DiscountsFascadeService } from 'src/app/appointments/domain/application-services/discounts-fascade.service';
import { AppState, IAppState } from 'src/app/common/app-state/app-state';
import { AppStateService } from 'src/app/common/app-state/app-state.service';
import { Role } from 'src/app/common/app-state/role';

interface IDiscountFormData {
  patientId: string,
  specialty: string,
  amountInPercentage: number
}

@Component({
  selector: 'create-coupon-form',
  templateUrl: './create-coupon-form.component.html',
  styleUrls: ['./create-coupon-form.component.css']
})

export class CreateCouponFormComponent {
  public discountForm: FormGroup;
  public specialities: Array<string> = ["Cardiology", "Pulmology", "Neurology"]; 
  public appState!: AppState;

  constructor(private discountsService: DiscountsFascadeService, private appStateService: AppStateService) 
  {
    this.discountForm = new FormGroup(
      {
        patientId: new FormControl(''),
        specialty: new FormControl(''),
        amountInPercentage: new FormControl('')
      }
    );

    this.appStateService.getAppState()
      .subscribe((appState: IAppState) => { this.appState = appState; });
  }

  public isStaffLoggedIn(): boolean {
    return this.appState?.hasRole(Role.Doctor) || this.appState?.hasRole(Role.Nurse);
  }

  public onSubmit(): void
  {
    const formData = this.discountForm.value as IDiscountFormData;

    if (!this.checkFormDataIsValid(formData))
      return;

    this.discountsService.createDiscount(formData.patientId, formData.specialty, formData.amountInPercentage)
      .pipe(
        take(1),
        catchError((err) => {
          if (err instanceof HttpErrorResponse) {
            window.alert('Failed to create Discount. \nPlease check your data validity.')
            console.error(err);
          }
          else
            window.alert('Failed to create Discount.')
          return of([])
        }),
      ).subscribe(() => { window.location.reload() });
  }

  private checkFormDataIsValid(formData: IDiscountFormData):boolean
  {
    if (formData == null || formData == undefined)
    {
      window.alert("Form data is not valid!"); 
      return false;
    } 
    
    if (formData.specialty == null || formData.specialty == undefined || formData.specialty.length == 0)
    {
      window.alert("Please choose valid specialty.");
      return false;
    }

    if (formData.patientId.length == 0)
    {
      window.alert("Please insert valid patient ID.");
      return false;
    }

    if (!this.checkNumericValue(formData.amountInPercentage) || formData.amountInPercentage > 100 || formData.amountInPercentage < 0)
    {
      window.alert("Please insert valid amount in percentage.");
      return false;
    }

    return true;
  }

  private checkNumericValue(value: number) : boolean
  {
    return !isNaN(Number(value));
  } 
}
