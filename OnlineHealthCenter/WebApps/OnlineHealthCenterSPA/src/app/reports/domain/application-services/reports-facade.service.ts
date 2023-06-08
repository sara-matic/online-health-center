import { Injectable } from '@angular/core';
import { ReportsService } from '../infrastructure/reports.service';
import { ICreateReportRequest } from '../models/create-report-request';
import { Observable, catchError, filter, map, of, switchMap, take } from 'rxjs';
import { IAppState } from 'src/app/common/app-state/app-state';
import { AppStateService } from 'src/app/common/app-state/app-state.service';
import { IShowReportResponse } from '../models/show-report-response';

@Injectable({
  providedIn: 'root'
})
export class ReportsFacadeService {
  constructor(private reportsService: ReportsService, private appStateService: AppStateService) { }

  public createReport(patientId: string, patientFirstName: string, patientLastName: string, 
    comment: string, diagnosis: string, prescription: string): Observable<string | null> {

      return this.appStateService.getAppState().pipe(
        take(1),
        map((appState: IAppState) => {
          const request: ICreateReportRequest = { 
            patientId, 
            patientFirstName, 
            patientLastName, 
            doctorId: appState.userId as string,
            doctorFirstName: appState.firstName as string, 
            doctorLastName: appState.lastName as string, 
            comment, 
            diagnosis, 
            prescription 
          };

          return request;
        }),
        switchMap((request: ICreateReportRequest) => {
          return this.reportsService.createReport(request);
        }),
        map (() => {
          return null;
        }),
        catchError((err) => {
          let errorMessage: string;
          if (err.status === 403) {
            errorMessage = 'To create report you need to be logged in as a doctor.';
          }
          else {
            errorMessage = 'Failed to create report.';
          }
          console.error(err);
          return of(errorMessage);
        })
      );
  }

  public getReportsByPatientId(patientId: string): Observable<IShowReportResponse[]> {
    return this.reportsService.getReportsByPatientId(patientId); 
  }

  public getReportsByDoctorId(doctorId: string): Observable<IShowReportResponse[]> {
    return this.reportsService.getReportsByDoctorId(doctorId); 
  }
}
