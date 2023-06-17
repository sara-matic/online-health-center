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

  public getAppointmentsByDoctorId(doctorId: string): Observable<Array<IAppointmentEntity>>
  {
    return this.appointmentsService.getAppointmentsByDoctorId(doctorId);
  }

  public getAllAppointments(): Observable<Array<IAppointmentEntity>>
  {
    return this.appointmentsService.getAllAppointments();
  }

  public approveAppointment(patientId: string, appointmentTime: string): Observable<boolean>
  {
    return this.appointmentsService.approveAppointment(patientId, appointmentTime);
  }

  public approveAppointmentById(appointmentId: string): Observable<boolean>
  {
    return this.appointmentsService.approveAppointmentById(appointmentId);
  }

  public cancelAppointment(patientId: string, appointmentTime: string): Observable<boolean>
  {
    return this.appointmentsService.cancelAppointment(patientId, appointmentTime);
  }
  
  public cancelAppointmentById(appointmentId: string): Observable<boolean>
  {
    return this.appointmentsService.cancelAppointmentById(appointmentId);
  }

  public applyDiscount(patientId: string, specialty: string): Observable<boolean>
  {
    return this.appointmentsService.applyDiscount(patientId, specialty);
  }

  public createAppointment(patientId : string, appointmentTime: string, specialty: string, doctorId: string, doctorName: string, patientName: string,
    requestCreatedBy: string, initialPrice: number, requestCreatedTime: string, requestStatus: string) : Observable<boolean>
  {
    return this.appointmentsService.createAppointment(patientId, appointmentTime, specialty, doctorId, doctorName, patientName, requestCreatedBy, initialPrice, requestCreatedTime, requestStatus);
  }

  public deleteDiscount(appointmentId: string): Observable<boolean>
  {
    return this.appointmentsService.deleteDiscount(appointmentId);
  }
}
