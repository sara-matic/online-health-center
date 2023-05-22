import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ImpressionsFascadeService } from '../../domain/application-services/impressions-fascade.service';
import { IImpressionEntity } from '../../domain/model/impressionEntity';
import { IDoctorEntity } from 'src/app/common/domain/model/doctorEntity';
import { EmployeesFascadeService } from 'src/app/common/domain/application-services/employees-fascade.service';

interface IImpressionFormData {
  doctorID: string;
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

  constructor(private impressionsService:  ImpressionsFascadeService, private employeeService: EmployeesFascadeService) {
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
        doctorID: entity.doctorID,
        patientID: entity.patientID,
        headline: entity.headline,
        content: entity.content,
        mark: entity.mark
      }

      uiDataCollection.push(impressionUIData);
    });

    return uiDataCollection;
  }
}
