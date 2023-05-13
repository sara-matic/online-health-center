import { Injectable } from '@angular/core';
import { DiscountsService } from '../infrastructure/discounts.service';
import { Observable } from 'rxjs';
import { IDiscountEntity } from '../model/discountEntity';

@Injectable({
  providedIn: 'root'
})
export class DiscountsFascadeService {

  constructor(private discountService: DiscountsService) { }

  public getDiscountsByPatientId(patientId: string): Observable<Array<IDiscountEntity>>
  {
    return this.discountService.getDiscountsByPatientId(patientId);
  }

  public applyDiscount(patientId: string, specialty: string): Observable<Array<IDiscountEntity>>
  {
    return this.applyDiscount(patientId, specialty);
  }

  public createDiscount(patientId: string, specialty: string, amountInPercentage: number): Observable<void>
  {
    return this.discountService.createDiscount(patientId, specialty, amountInPercentage);
  }
}
