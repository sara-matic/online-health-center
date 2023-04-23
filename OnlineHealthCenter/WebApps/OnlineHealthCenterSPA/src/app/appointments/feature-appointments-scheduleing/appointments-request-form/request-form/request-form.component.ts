import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IApointmentTime } from 'src/app/appointments/domain/appointmentTimeEntity';
import { IDoctorEntity } from 'src/app/appointments/domain/doctor-entity';

interface IRequestFormData
{
  doctor: string;
  patientID: string;
  initialPrice: number;
  appointmentTime: string;
}

@Component({
  selector: 'app-request-form',
  
  templateUrl: './request-form.component.html',
  styleUrls: ['./request-form.component.css']
})

export class RequestFormComponent {
  public requestForm: FormGroup;

  //Hard coded data:
  public doctors: Array<IDoctorEntity> = [{id: "1", firstName: "Doctor", "lastName": "1", specialty: "Cardilology", title: "Specialist"},
  {id: "2", firstName: "Doctor", "lastName": "2", specialty: "Pulmology", title: "Specialist"},
  {id: "3", firstName: "Doctor", "lastName": "3", specialty: "Cardilology", title: "Primarius"},
  {id: "1", firstName: "Doctor", "lastName": "1", specialty: "Cardilology", title: "Full Professor && Specialist"}];

  public doctorTimes: Array<IApointmentTime> = []; 

  constructor()
  {
    this.requestForm = new FormGroup(
      {
        doctor: new FormControl(''),
        patientID: new FormControl(''),
        initialPrice: new FormControl(''),
        appointmentTime: new FormControl('')
      }
    );
  }
 
  public onRequestSubmit(): void
  {
    const data: IRequestFormData = this.requestForm.value as IRequestFormData;

    if (!this.pageDataIsValid(data))
      return;

    window.confirm(
      "\nPatient ID: " + data.patientID + "\nDoctor: " + data.doctor + "\nAppointment time: " + data.appointmentTime + "\nInitial price: " + data.initialPrice
      + "\n\nClick OK to confirm request or Cancel it."
      );
  }

  public onSelectionChanged(): void
  {
    //Hard coded data:
    this.doctorTimes = [{time: "time11"}, {time: "time21"}, {time: "time31"}];  
  }

  private pageDataIsValid(data: IRequestFormData): boolean
  {
    if (data.doctor == null || data.doctor.length == 0)
    {
      window.alert("Please choose doctor in order to continue.");
      return false;
    }

    if (data.appointmentTime == null || data.appointmentTime.length == 0)
    {
      window.alert("Please select appointment time in order to continue.");
      return false;
    }

    if (data.patientID == null || data.patientID.length == 0)
    {
      window.alert("Please insert valid patient ID in order to continue.");
      return false;
    }

    return true;
  }
}
