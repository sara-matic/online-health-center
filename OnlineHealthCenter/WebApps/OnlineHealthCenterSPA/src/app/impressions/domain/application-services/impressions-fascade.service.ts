import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ImpressionsService } from '../infrastructure/impressions.service';
import { IImpressionEntity } from '../model/impressionEntity';

@Injectable({
  providedIn: 'root'
})
export class ImpressionsFascadeService {

  constructor(private impressionsService: ImpressionsService) { }

  public getImpressions(): Observable<Array<IImpressionEntity>>
  {
    return this.impressionsService.getImpressions();
  }

  public getImpressionsByPatientId(patientID: string): Observable<Array<IImpressionEntity>>
  {
    return this.impressionsService.getImpressionsByPatientId(patientID);
  }

  public deleteImpression(id: string): Observable<boolean>
  {
    return this.impressionsService.deleteImpression(id);
  }
}
