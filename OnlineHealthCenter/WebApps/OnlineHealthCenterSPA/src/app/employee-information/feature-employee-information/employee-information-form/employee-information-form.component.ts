import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { EmployeesFascadeService } from 'src/app/common/domain/application-services/employees-fascade.service';
import { IDoctorEntity } from 'src/app/common/domain/model/doctorEntity';
import { ImpressionsFascadeService } from 'src/app/impressions/domain/application-services/impressions-fascade.service';
import { IImpressionEntity } from 'src/app/impressions/domain/model/impressionEntity';

interface IEmployeeInformationFormData {
  id: string;
  firstName: string;
  lastName: string;
  title: string;
  medicalSpecialty: string;
  biography: string;
  mark: number;
}

@Component({
  selector: 'app-employee-information-form',
  templateUrl: './employee-information-form.component.html',
  styleUrls: ['./employee-information-form.component.css']
})
export class EmployeeInformationFormComponent {

  public EmployeeInformationForm: FormGroup;

  public doctors: Array<IDoctorEntity> = this.getDoctors();

  public allImpressions: Array<IImpressionEntity> = this.getImpressions();

  public doctor? : IEmployeeInformationFormData;
  public impressions? : IImpressionEntity[];

  constructor(private employeesService: EmployeesFascadeService, private impressionsService: ImpressionsFascadeService) {

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

  private getImpressions(): Array<IImpressionEntity> {
    this.impressionsService.getImpressions().subscribe(
      (impressions: Array<IImpressionEntity>) => {
        this.allImpressions = impressions;
      }
    );
    return this.allImpressions;
  }

  public onSelectionChanged(): void
  {
      const data: IEmployeeInformationFormData = this.EmployeeInformationForm.value as IEmployeeInformationFormData;
      this.doctor = this.doctors.filter(d => d.id == data.id)[0] as IDoctorEntity;
      this.impressions = this.allImpressions.filter(imp => imp.doctorID == data.id)
      this.impressions.map(i => i.impressionDateTime = new Date(i.impressionDateTime).toLocaleString());
  }

}
