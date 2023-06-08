import { Injectable } from '@angular/core';
import { ICreateReportRequest } from '../models/create-report-request';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { IShowReportResponse } from '../models/show-report-response';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {
  private readonly url: string = 'http://localhost:8003/api/v1/Report';

  constructor(private httpClient: HttpClient) { }

  public createReport(request: ICreateReportRequest): Observable<Object> {
    return this.httpClient.post(`${this.url}/CreateReport`, request);
  }

  public getReportsByPatientId(patientId: string): Observable<IShowReportResponse[]> {
    return this.httpClient.get<IShowReportResponse[]>(`${this.url}/GetReportsByPatientId/${patientId}`);
  }

  public getReportsByDoctorId(doctorId: string): Observable<IShowReportResponse[]> {
    return this.httpClient.get<IShowReportResponse[]>(`${this.url}/GetReportsByDoctorId/${doctorId}`);
  }

  public getReportsByPatientAndDoctorId(patientId: string, doctorId: string): Observable<IShowReportResponse[]> {
    return this.httpClient.get<IShowReportResponse[]>(`${this.url}/GetReportsByPatientAndDoctorId/${doctorId}/${patientId}`);
  }
}
