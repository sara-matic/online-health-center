import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IDoctorEntity } from 'src/app/common/domain/model/doctorEntity';
import { EmployeesFascadeService } from 'src/app/common/domain/application-services/employees-fascade.service';
import { ImpressionsFascadeService } from '../../domain/application-services/impressions-fascade.service';

interface IAddImpressionFormData
{
  doctor: string;
  patientID: string;
  headline: string;
  content: string;
  mark: number;
}

@Component({
  selector: 'app-add-impression-form',
  templateUrl: './add-impression-form.component.html',
  styleUrls: ['./add-impression-form.component.css']
})

export class AddImpressionFormComponent {

  public addImpressionForm: FormGroup;

  public doctors: Array<IDoctorEntity> = this.getDoctors();

  constructor(private employeesService : EmployeesFascadeService, private impressionsService : ImpressionsFascadeService) {
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

  private getDoctors(): Array<IDoctorEntity> {
    this.employeesService.getDoctors().subscribe(
      (doctors: Array<IDoctorEntity>) => {
        this.doctors = doctors;
      });
      
    return this.doctors;
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
