import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IImpressionEntity } from '../model/impressionEntity';

@Injectable({
  providedIn: 'root'
})
export class ImpressionsService {

  constructor(private httpClient: HttpClient) { }

  private readonly commonPath = "http://localhost:8002/api/v1/Impression/";

  public getImpressions(): Observable<Array<IImpressionEntity>> 
  {
    const connectionString = this.commonPath + 'GetImpressions';
    return this.httpClient.get<Array<IImpressionEntity>>(connectionString);
  }

}
