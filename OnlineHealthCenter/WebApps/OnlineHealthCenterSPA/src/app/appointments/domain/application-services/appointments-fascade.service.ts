import { Injectable } from '@angular/core';
import { AppointmentsService } from '../infrastructure/appointments.service';
import { Observable } from 'rxjs';
import { IAppointmentEntity } from '../model/appointmentEntity';

@Injectable({
  providedIn: 'root'
})

export class AppointmentsFascadeService {

  constructor(private appointmentsService: AppointmentsService) {}

  public getAppointmentsByPatientId(patientId: string): Observable<Array<IAppointmentEntity>>
  {
    return this.appointmentsService.getAppointmentsByPatientId(patientId);
  }
}
