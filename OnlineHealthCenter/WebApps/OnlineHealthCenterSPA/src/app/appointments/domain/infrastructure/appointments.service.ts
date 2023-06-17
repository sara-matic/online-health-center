import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IAppointmentEntity } from '../model/appointmentEntity';
import { IAppointmentApproveRequest } from '../model/appointment-approve-request';
import { IAppointmentCancelRequest } from '../model/appoinement-cancel-request';
import { IAppointmentDeleteRequest } from '../model/appointment-delete-request';
import { IApplyDiscuntRequest } from '../model/apply-discount-request';
import { ICreateDiscountRequest } from '../model/ICreateDiscountRequest';
import { ICreateAppointmentRequest } from '../model/ICreateAppointmentRequest';

@Injectable({
  providedIn: 'root'
})

export class AppointmentsService {

  private readonly commonPath: string = "http://localhost:8005/api/v1/Appointments/";

  constructor(private httpClient: HttpClient){ }

  public getAppointmentsByPatientId(patientId: string): Observable<Array<IAppointmentEntity>>
  {
    const connectionString = this.commonPath + 'GetAppointmentsByPatientId/' + patientId+ "?patientId=" + patientId;
    return this.httpClient.get<Array<IAppointmentEntity>>(connectionString);
  }

  public getAppointmentsByDoctorId(doctorId: string): Observable<Array<IAppointmentEntity>>
  {
    const connectionString = this.commonPath + 'GetAppointmentsByDoctorId/' + doctorId+ "?doctorId=" + doctorId;
    return this.httpClient.get<Array<IAppointmentEntity>>(connectionString);
  }

  public getAllAppointments(): Observable<Array<IAppointmentEntity>>
  {
    const connectionString = this.commonPath + 'GetAllAppointments';
    return this.httpClient.get<Array<IAppointmentEntity>>(connectionString);
  }
  
  public approveAppointment(patientId: string, appointmentTime: string): Observable<boolean>
  {
    const connectionString = this.commonPath + 'ApproveAppointment';
    const approveRequest: IAppointmentApproveRequest = {patientId: patientId, appointmentTime: appointmentTime};
    
    return this.httpClient.put<boolean>(connectionString, approveRequest);
  }

  public approveAppointmentById(appointmentId: string): Observable<boolean>
  {
    const connectionString = this.commonPath + 'ApproveAppointment/' + appointmentId + '?appointmentId=' + appointmentId;
    return this.httpClient.put<boolean>(connectionString, appointmentId);
  }

  public cancelAppointment(patientId: string, appointmentTime: string): Observable<boolean>
  {
    const connectionString = this.commonPath + 'CancelAppointment';
    const cancelRequest: IAppointmentCancelRequest = {patientId: patientId, appointmentTime: appointmentTime};
    
    return this.httpClient.put<boolean>(connectionString, cancelRequest);
  }

  public cancelAppointmentById(appointmentId: string): Observable<boolean>
  {
    const connectionString = this.commonPath + 'CancelAppointment/' + appointmentId + '?appointmentId=' + appointmentId;
    return this.httpClient.put<boolean>(connectionString, appointmentId);
  }

  public applyDiscount(patientId: string, specialty: string): Observable<boolean>
  {
    const connectionString = this.commonPath + 'ApplyDiscountAmount/' + patientId + "/" + specialty + "?patientId=" + patientId + "?specialty=" + specialty;
    const applyDiscountRequest: IApplyDiscuntRequest = {patientId: patientId, specialty: specialty};
    
    return this.httpClient.put<boolean>(connectionString, applyDiscountRequest);
  }

  public createAppointment(patientId : string, appointmentTime: string, specialty: string, doctorId: string, doctorName: string, patientName: string,
    requestCreatedBy: string, initialPrice: number, requestCreatedTime: string, requestStatus: string) : Observable<boolean>
  {
    const connectionString = this.commonPath + 'CreateAppointment';
    const createRequest: ICreateAppointmentRequest = {
      patientId: patientId,
      appointmentTime: appointmentTime,
      appointmentId: crypto.randomUUID(),
      patientName: patientName,
      doctorName: doctorName,
      specialty: specialty,
      doctorId: doctorId,
      requestCreatedBy: requestCreatedBy,
      initialPrice: initialPrice,
      requestCreatedTime: requestCreatedTime,
      requestStatus: requestStatus
    };

    return this.httpClient.post<boolean>(connectionString, createRequest);
  }

  public deleteDiscount(appointmentId: string): Observable<boolean>
  {
    const connectionString = this.commonPath + 'DeleteAppointment/' + appointmentId;
    return this.httpClient.delete<boolean>(connectionString);
  }
}
