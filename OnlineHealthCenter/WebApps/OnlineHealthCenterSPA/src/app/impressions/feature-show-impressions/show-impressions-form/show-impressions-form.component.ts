import { Component } from '@angular/core';
import { ImpressionsFascadeService } from '../../domain/application-services/impressions-fascade.service';
import { IImpressionEntity } from '../../domain/model/impressionEntity';
import { IDoctorEntity } from 'src/app/common/domain/model/doctorEntity';
import { EmployeesFascadeService } from 'src/app/common/domain/application-services/employees-fascade.service';
import { FormControl, FormGroup } from '@angular/forms';

interface IImpressionFormData {
  doctor: string;
  patientID: string;
  headline: string;
  content: string;
  mark: number;
}

@Component({
  selector: 'app-show-impressions-form',
  templateUrl: './show-impressions-form.component.html',
  styleUrls: ['./show-impressions-form.component.css']
})
export class ShowImpressionsFormComponent {

  public showImpressionForm: FormGroup;

  public allImpressions: Array<IImpressionFormData> = this.getImpressionsByPatientId("a15a4178-2964-4973-b1fe-425ef1fdc0a4");

  public doctors: Array<IDoctorEntity> = this.getDoctors();

  public doctor?: IDoctorEntity;

  public impressions?: Array<IImpressionFormData>;

  constructor(private impressionsService:  ImpressionsFascadeService, private employeesService: EmployeesFascadeService) {
    this.showImpressionForm = new FormGroup({
      doctor: new FormControl(''),
      patientID: new FormControl(''),
      headline: new FormControl(''),
      content: new FormControl(''),
      mark: new FormControl('')
    })
  }

  private getImpressionsByPatientId(patientID: string): Array<IImpressionFormData> {
    this.impressionsService.getImpressionsByPatientId(patientID).subscribe((impressions: Array<IImpressionEntity>) => {
      this.allImpressions = this.getFormDataFromImpressionEntities(impressions);
    })

    return this.allImpressions;
  }

  private getFormDataFromImpressionEntities(entities: Array<IImpressionEntity>): Array<IImpressionFormData> {
    var uiDataCollection = Array<IImpressionFormData>();

    entities.forEach(entity => {
      const impressionUIData: IImpressionFormData = {
        doctor: entity.doctorID,
        patientID: entity.patientID,
        headline: entity.headline,
        content: entity.content,
        mark: entity.mark
      }

      uiDataCollection.push(impressionUIData);
    });

    return uiDataCollection;
  }

  private getDoctors(): Array<IDoctorEntity> {
    this.employeesService.getDoctors().subscribe(
      (doctors: Array<IDoctorEntity>) => {
        this.doctors = doctors;
      });
      
    return this.doctors;
  }

  private getSelectedDoctor(): IDoctorEntity
  {
    const data: IImpressionFormData = this.showImpressionForm.value as IImpressionFormData;
    return this.doctors.filter(doc => doc.id == data.doctor)[0] as IDoctorEntity;
  }

  public onSelectionChanged(): void
  {
    const selectedDoctor = this.getSelectedDoctor();
    this.doctor = selectedDoctor;
    this.impressions = this.allImpressions.filter(i => i.doctor == selectedDoctor.id);
  }

}
