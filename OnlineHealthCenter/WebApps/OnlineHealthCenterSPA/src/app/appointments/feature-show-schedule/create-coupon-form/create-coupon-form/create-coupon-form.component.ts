import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DiscountsFascadeService } from 'src/app/appointments/domain/application-services/discounts-fascade.service';

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

  constructor(private discountsService: DiscountsFascadeService) 
  {
    this.discountForm = new FormGroup(
      {
        patientId: new FormControl(''),
        specialty: new FormControl(''),
        amountInPercentage: new FormControl('')
      }
    );
  }

  public onSubmit(): void
  {
    const formData = this.discountForm.value as IDiscountFormData;

    if (!this.checkFormDataIsValid(formData))
      return;

    this.discountsService.createDiscount(formData.patientId, formData.specialty, formData.amountInPercentage)
      .subscribe(() => {window.location.reload()});
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
