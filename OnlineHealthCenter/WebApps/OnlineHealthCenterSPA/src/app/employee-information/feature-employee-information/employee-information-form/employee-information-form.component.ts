import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { EmployeesFascadeService } from 'src/app/common/domain/application-services/employees-fascade.service';
import { IDoctorEntity } from 'src/app/common/domain/model/doctorEntity';

interface IEmployeeInformationFormData {
  id: string;
  firstName: string;
  lastName: string;
  title: string;
  medicalSpecialty: string;
  biography: string;
  mark: number;
}

interface IImpressionData {
  id: string;
  headline: string
  content: string
  mark: number
}

@Component({
  selector: 'app-employee-information-form',
  templateUrl: './employee-information-form.component.html',
  styleUrls: ['./employee-information-form.component.css']
})
export class EmployeeInformationFormComponent {

  public EmployeeInformationForm: FormGroup;

  public doctors: Array<IDoctorEntity> = this.getDoctors();

  public allImpressions: Array<IImpressionData> = [{id: "558cd9e5-4ca9-4aea-8b6c-7f7b2d4e01ba", headline: "Nice experience", content: "Doctor was very nice and kind", mark: 9},
  {id: "558cd9e5-4ca9-4aea-8b6c-7f7b2d4e01ba", headline: "Very good", content: "Doctor was really patient and calm, I had the best experience", mark: 10},
  {id: "2", headline: "Amazing!", content: "Exam was really great", mark: 9}];

  public doctor? : IEmployeeInformationFormData;
  public impressions? : IImpressionData[];

  constructor(private employeesService: EmployeesFascadeService) {

    this.EmployeeInformationForm = new FormGroup(
      {
        id: new FormControl(''),
        firstName: new FormControl(''),
        lastName: new FormControl(''),
        title: new FormControl(''),
        specialty: new FormControl(''),
        biography: new FormControl(''),
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

  public onSelectionChanged(): void
  {
      const data: IEmployeeInformationFormData = this.EmployeeInformationForm.value as IEmployeeInformationFormData;
      this.doctor = this.doctors.filter(d => d.id == data.id)[0] as IDoctorEntity;
      this.impressions = this.allImpressions.filter(imp => imp.id == data.id)
  }



}
