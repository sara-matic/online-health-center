import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IImpressionEntity } from '../model/impressionEntity';
import { IAddImpressionRequest } from '../model/IAddImpressionRequest';

@Injectable({
  providedIn: 'root'
})
export class ImpressionsService {

  constructor(private httpClient: HttpClient) { }

  private readonly commonPath = "http://localhost:8002/api/v1/Impression/";

  public getImpressions(): Observable<Array<IImpressionEntity>> 
  {
    const connectionString = this.commonPath + 'GetImpressionsWithId';
    return this.httpClient.get<Array<IImpressionEntity>>(connectionString);
  }

  public getImpressionsByPatientId(patientID: string): Observable<Array<IImpressionEntity>> 
  {
    const connectionString = this.commonPath + 'GetImpressionsByPatientId/' + patientID + "?patientID="+patientID;
    return this.httpClient.get<Array<IImpressionEntity>>(connectionString);
  }

  public addImpression(request: IAddImpressionRequest): Observable<Object> {
    const connectionString = this.commonPath + 'AddImpression';
    return this.httpClient.post(connectionString, request);
  }

  public deleteImpression(id: string): Observable<boolean>
  {
    const connectionString = this.commonPath + 'DeleteImpressionById/' + id + "?id" + id;
    return this.httpClient.delete<boolean>(connectionString);
  }
}
