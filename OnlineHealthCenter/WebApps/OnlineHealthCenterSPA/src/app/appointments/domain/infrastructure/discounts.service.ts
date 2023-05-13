import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IDiscountEntity } from '../model/discountEntity';
import { ICreateDiscountRequest } from '../model/ICreateDiscountRequest';

@Injectable({
  providedIn: 'root'
})
export class DiscountsService {

  private readonly commonPath = "http://localhost:8001/api/v1/Discount";
  constructor(private httpClient: HttpClient) { }

  public getDiscountsByPatientId(patientId: string): Observable<Array<IDiscountEntity>> {
    const connectionString = this.commonPath + '/GetAllPatientsDiscounts/' + patientId + "?patientId=" + patientId;
    return this.httpClient.get<Array<IDiscountEntity>>(connectionString);
  }

  //TODO: test this feature before using it!
  public createDiscount(patientId: string, specialty: string, amountInPercentage: number): Observable<void> {
    const connectionString = this.commonPath + '/CreateDiscount';
    const discountId = this.generateDiscountId();
    const createRequest: ICreateDiscountRequest = { patientId: patientId, specialty: specialty, amountInPercentage: amountInPercentage, id: discountId};
    return this.httpClient.post<void>(connectionString, createRequest);
  }

  private generateDiscountId(): string {
    var result = "";
    var hexChar = "0123456789abcdef";
    for (var i = 0; i < 24; i++) {
      result += hexChar.charAt(Math.floor(Math.random() * hexChar.length));
    }

    return result;
  }
}
