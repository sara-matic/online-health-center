import { Injectable } from '@angular/core';
import { Observable, catchError, map, of, pairwise, switchMap, take } from 'rxjs';
import { ImpressionsService } from '../infrastructure/impressions.service';
import { IImpressionEntity } from '../model/impressionEntity';
import { AppStateService } from 'src/app/common/app-state/app-state.service';
import { IAppState } from 'src/app/common/app-state/app-state';
import { IAddImpressionRequest } from '../model/IAddImpressionRequest';

@Injectable({
  providedIn: 'root'
})
export class ImpressionsFascadeService {

  constructor(private impressionsService: ImpressionsService, private appStateService: AppStateService) { }

  public getImpressions(): Observable<Array<IImpressionEntity>>
  {
    return this.impressionsService.getImpressions();
  }

  public getImpressionsByPatientId(patientID: string): Observable<Array<IImpressionEntity>>
  {
    return this.impressionsService.getImpressionsByPatientId(patientID);
  }

  public addImpression(doctorID: string, headline: string, content: string, mark: number): Observable<string | null> {
    return this.appStateService.getAppState().pipe(
      take(1),
      map((appState: IAppState) => {
        const request: IAddImpressionRequest = { 
          doctorID,
          patientID: appState.userId as string,
          headline,
          content,
          mark
        };

        return request;
      }),
      switchMap((request: IAddImpressionRequest) => {
        return this.impressionsService.addImpression(request);
      }),
      map (() => {
        return null;
      }),
      catchError((err) => {
        let errorMessage: string;
        if (err.status === 403) {
          errorMessage = 'To create impression you need to be logged in as a patient.';
        }
        else {
          errorMessage = 'Failed to create impression.';
        }
        console.error(err);
        return of(errorMessage);
      })
    );
  }

  public deleteImpression(id: string): Observable<boolean>
  {
    return this.impressionsService.deleteImpression(id);
  }
}
