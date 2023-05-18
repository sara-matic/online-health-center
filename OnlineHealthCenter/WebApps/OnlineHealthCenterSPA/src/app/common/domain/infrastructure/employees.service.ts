import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IDoctorEntity } from '../model/doctorEntity';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  constructor(private httpClient: HttpClient) { }

  private readonly commonPath = "http://localhost:8000/api/v1/Doctor";

  public getDoctors(): Observable<Array<IDoctorEntity>> {
    const connectionString = this.commonPath + '/GetDoctors';
    return this.httpClient.get<Array<IDoctorEntity>>(connectionString);
  }
}
