import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IDoctorEntity } from '../../domain/doctorEntity';

interface IAddImpressionFormData
{
  doctor: string;
  patientID: string;
  headline: string
  content: string
  mark: number
}

@Component({
  selector: 'app-add-impression-form',
  templateUrl: './add-impression-form.component.html',
  styleUrls: ['./add-impression-form.component.css']
})

export class AddImpressionFormComponent {

  public addImpressionForm: FormGroup;

  public doctors: Array<IDoctorEntity> = [{id: "1", firstName: "Doctor", "lastName": "1", specialty: "Cardilology", title: "Specialist"},
  {id: "2", firstName: "Doctor", "lastName": "2", specialty: "Pulmology", title: "Specialist"},
  {id: "3", firstName: "Doctor", "lastName": "3", specialty: "Cardilology", title: "Primarius"},
  {id: "1", firstName: "Doctor", "lastName": "1", specialty: "Cardilology", title: "Full Professor && Specialist"}];

  constructor() {
    this.addImpressionForm = new FormGroup(
      {
        doctor: new FormControl(''),
        patientID: new FormControl(''),
        headline: new FormControl(''),
        content: new FormControl(''),
        mark: new FormControl('')
      }
    );
  }

  public onAddImpressionSubmit(): void
  {
    const data: IAddImpressionFormData = this.addImpressionForm.value as IAddImpressionFormData;

    if (!this.pageDataIsValid(data)) {
      return;
    }

    window.confirm(
      "\nDoctor: " + data.doctor + "\nMark: " + data.mark
      + "\n\nClick OK to confirm."
      );
  }

  private pageDataIsValid(data: IAddImpressionFormData): boolean
  {
    if (data.doctor == null || data.doctor.length == 0)
    {
      window.alert("Please choose doctor in order to continue.");
      return false;
    }

    if (data.patientID == null || data.patientID.length == 0)
    {
      window.alert("Please insert valid patient ID in order to continue.");
      return false;
    }

    if (data.mark == null)
    {
      window.alert("Please leave a mark in order to continue.");
      return false;
    }

    return true;
  }

}
