import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AppointmentRequestStatus } from '../../domain/model/appointment-status';
import { AppointmentsFascadeService } from '../../domain/application-services/appointments-fascade.service';
import { IAppointmentEntity } from '../../domain/model/appointmentEntity';

interface IScheduleFormData {
  appointmentID: string;
  doctor: string;
  patientName: string;
  initialPrice: number;
  appointmentTime: string;
  discount?: number;
  appointmentStatus?: string;
}

@Component({
  selector: 'app-schedule-form',
  templateUrl: './schedule-form.component.html',
  styleUrls: ['./schedule-form.component.css']
})
export class ScheduleFormComponent {
  public scheduleForm: FormGroup;

  //Hard coded data:
  public appointmentsCollection: Array<IScheduleFormData> = this.getAppointmentsByPatientId("8de0295d-75de-4bba-ade8-43abc66b3103"); //TODO: replace hard coded data with real patientId

  public selectedAppointment?: IScheduleFormData;

  constructor(private service: AppointmentsFascadeService) {
    this.scheduleForm = new FormGroup(
      {
        appointmentID: new FormControl(''),
        patientName: new FormControl(''),
        doctor: new FormControl(''),
        patientID: new FormControl(''),
        initialPrice: new FormControl(''),
        appointmentTime: new FormControl(''),
        discount: new FormControl(''),
        requestStatus: new FormControl('')
      }
    );
  }

  private getAppointmentsByPatientId(patientID: string): Array<IScheduleFormData>
  {
    this.service.getAppointmentsByPatientId(patientID).subscribe((appointments: Array<IAppointmentEntity>) => {
      this.appointmentsCollection = this.getFormDataFromAppointmentEntities(appointments);
    });

    return this.appointmentsCollection;
  } 

  private getFormDataFromAppointmentEntities(entities: Array<IAppointmentEntity>): Array<IScheduleFormData>
  {
    var uiDataCollection = Array<IScheduleFormData>();

    entities.forEach(entity => {

      const appointmentUIData: IScheduleFormData = { appointmentID: entity.appointmentId,
      doctor: "dr NN" + "-" + entity.specialty,
      patientName: "NN",
      initialPrice: entity.initialPrice,
      appointmentTime: new Date(entity.appointmentTime).toLocaleString(),
      discount: 0,
      appointmentStatus: new AppointmentRequestStatus(entity.appointmentRequestStatus.requestStatus).getRequestStatusDescription()}

      uiDataCollection.push(appointmentUIData);
    });

    return uiDataCollection;
  }

  public onSelectionChanged(): void {
    const data: IScheduleFormData = this.scheduleForm.value as IScheduleFormData;
    this.selectedAppointment = this.appointmentsCollection.filter(apt => apt.appointmentID === data.appointmentID)[0];
  }

  public onAppointmentCancelationRequested(): void {

    if (this.selectedAppointment == null || this.selectedAppointment.appointmentID.length == 0) {
      window.alert("You must select valid appointment!");
      return;
    }

    if (window.confirm("Do you really want to cancel this appointment?\n\n Click OK to confirm or cancel to go back.")) {
      //TODO
    }
  }

  public onApplyDiscountRequested(): void
  {
    window.alert("TODO");
  }

  public onApproveRequested(): void
  {
    window.alert("TODO");
  }
}
