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

  public approveAppointment(patientId: string, appointmentTime: string): Observable<boolean>
  {
    return this.appointmentsService.approveAppointment(patientId, appointmentTime);
  }

  public cancelAppointment(patientId: string, appointmentTime: string): Observable<boolean>
  {
    return this.appointmentsService.cancelAppointment(patientId, appointmentTime);
  }
}
