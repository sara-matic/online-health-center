import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IRegistrationRequest } from '../model/registration-request';
import { Observable, of, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {
  private readonly patientUrl: string = 'http://localhost:4000/api/v1/PatientRegistration/RegisterPatient';
  private readonly staffUrl: string = 'http://localhost:4000/api/v1/StaffRegistration';

  constructor(private httpClient: HttpClient) { }

  public register(request: IRegistrationRequest, role: string): Observable<Object> {
    switch (role) {
      case 'Patient': {
        return this.registerPatient(request);
        break;
      }
      case 'Doctor': {
        return this.registerDoctor(request);
        break;
      }
      case 'Nurse': {
        return this.registerNurse(request);
        break;
      }
      default:
        console.error('Invalid role specified');
        return throwError(() => new Error('Invalid role'));
    }
  }

  public registerPatient(request: IRegistrationRequest): Observable<Object> {
    return this.httpClient.post(this.patientUrl, request);
  }

  public registerDoctor(request: IRegistrationRequest): Observable<Object> {
    return this.httpClient.post(`${this.staffUrl}/RegisterDoctor`, request);
  }

  public registerNurse(request: IRegistrationRequest): Observable<Object> {
    return this.httpClient.post(`${this.staffUrl}/RegisterNurse`, request);
  }
}