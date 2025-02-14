import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAddDoctorRequest } from '../model/IAddDoctorRequest';
import { IUpdateDoctorRequest } from '../model/IUpdateDoctorRequest';

@Injectable({
  providedIn: 'root'
})
export class EmployeeInformationService {

  constructor(private httpClient: HttpClient) { }

  private readonly commonPath = "http://localhost:8000/api/v1/Doctor/";

  public addDoctor(request: IAddDoctorRequest): Observable<Object> {
    const connectionString = this.commonPath + 'AddDoctor';
    return this.httpClient.post(connectionString, request);
  }

  public updateDoctor(request: IUpdateDoctorRequest): Observable<Object> {
    const connectionString = this.commonPath + 'UpdateDoctor';
    return this.httpClient.put(connectionString, request);
  }

  public deleteDoctor(id: string): Observable<boolean>
  {
    const connectionString = this.commonPath + 'DeleteDoctor/' + id;
    return this.httpClient.delete<boolean>(connectionString);
  }
}
