import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IDoctorEntity } from 'src/app/common/domain/model/doctorEntity';
import { EmployeesFascadeService } from 'src/app/common/domain/application-services/employees-fascade.service';
import { ImpressionsFascadeService } from '../../domain/application-services/impressions-fascade.service';
import { IAppState } from 'src/app/common/app-state/app-state';
import { Observable, catchError, map, switchMap, take } from 'rxjs';
import { AppStateService } from 'src/app/common/app-state/app-state.service';
import { NONE_TYPE } from '@angular/compiler';

interface IAddImpressionFormData
{
  doctor: string;
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

  public doctor? : IDoctorEntity;

  public appState$!: Observable<IAppState>;

  constructor(private employeesService : EmployeesFascadeService, private impressionsService : ImpressionsFascadeService,
    private appStateService: AppStateService) {
    this.addImpressionForm = new FormGroup(
      {
        doctor: new FormControl(''),
        headline: new FormControl(''),
        content: new FormControl(''),
        mark: new FormControl('')
      }
    );
    this.appState$ = this.appStateService.getAppState();
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

    const selectedDoctor: IDoctorEntity = this.doctors.filter(doc => doc.id == data.doctor)[0];

    const headline = data.headline;
    const content = data.content;
    const mark = data.mark;

    this.impressionsService.addImpression(selectedDoctor.id, headline, content, mark)
    .subscribe((errorMessage: string | null) => {
      if (errorMessage !== null) {
        window.alert(errorMessage);
      }
      else {
        window.alert('Impression added successfully.');
        this.addImpressionForm.reset();
      }
    });

        window.location.reload();
    }
  
  private pageDataIsValid(data: IAddImpressionFormData): boolean
  {
    if (data.doctor == null || data.doctor.length == 0)
    {
      window.alert("Please choose doctor in order to continue.");
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
