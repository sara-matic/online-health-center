import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AppointmentStatus } from '../../domain/appointment-status';

interface IScheduleFormData
{
  appointmentID: string;
  doctor: string;
  patientName: string;
  initialPrice: number;
  appointmentTime: string;
  discount?: number;
  appointmentStatus: AppointmentStatus;
}

@Component({
  selector: 'app-schedule-form',
  templateUrl: './schedule-form.component.html',
  styleUrls: ['./schedule-form.component.css']
})
export class ScheduleFormComponent {
  public scheduleForm: FormGroup;

  //Hard coded data:
  public appointments: Array<IScheduleFormData> = [
    {appointmentID: "1", doctor: "doctor 1", patientName: "patient 1", initialPrice:5500, appointmentTime: new Date('Monday, June 15, 2023 1:45 PM').toLocaleString(), discount: 30, appointmentStatus: AppointmentStatus.Approved},
    {appointmentID: "2", doctor: "doctor 2", patientName: "patient 2", initialPrice:4700, appointmentTime: new Date('Tuesday, June 15, 2023 1:45 PM').toLocaleString(),  appointmentStatus: AppointmentStatus.WaitingForAnswer},
    {appointmentID: "3", doctor: "doctor 3", patientName: "patient 3", initialPrice:3200, appointmentTime: new Date('Monday, June 8, 2023 1:45 PM').toLocaleString(), discount: 50,  appointmentStatus: AppointmentStatus.WaitingForAnswer},
    {appointmentID: "4", doctor: "doctor 4", patientName: "patient 4", initialPrice:7000, appointmentTime: new Date('Friday, June 15, 2023 1:45 PM').toLocaleString(), discount: 20,  appointmentStatus: AppointmentStatus.Approved},
  ];

  public appointment?: IScheduleFormData;

  constructor()
  {
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

  public onSelectionChanged(): void
  {
      const data: IScheduleFormData = this.scheduleForm.value as IScheduleFormData;
      this.appointment = this.appointments.filter(apt => apt.appointmentID === data.appointmentID)[0];
  }

  public onAppointmentCancelationRequested(): void 
  {
     if (this.appointment == null || this.appointment.appointmentID.length == 0)
     {
      window.alert("You must select valid appointment!");
      return;
     }

     if(window.confirm("Do you really want to cancel this appointment?\n\n Click OK to confirm or cancel to go back."))
     {
      //TODO
     }
  }
}
