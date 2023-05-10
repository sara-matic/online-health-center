import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IAppointmentEntity } from '../model/appointmentEntity';

@Injectable({
  providedIn: 'root'
})

export class AppointmentsService {

  constructor(private httpClient: HttpClient){ }

  public getAppointmentsByPatientId(patientId: string): Observable<Array<IAppointmentEntity>>
  {
    //TODO: Update the connection string to be like *localhost:8005* after setting up CORS
    const connectionString = 'http://localhost:5005/api/v1/Appointments/GetAppointmentsByPatientId/'+patientId+"?patientId="+patientId;
    return this.httpClient.get<Array<IAppointmentEntity>>(connectionString);
  }  
}
