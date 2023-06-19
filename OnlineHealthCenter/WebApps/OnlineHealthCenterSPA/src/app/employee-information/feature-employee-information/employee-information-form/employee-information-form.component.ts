import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { EmployeesFascadeService } from 'src/app/common/domain/application-services/employees-fascade.service';
import { IDoctorEntity } from 'src/app/common/domain/model/doctorEntity';
import { ImpressionsFascadeService } from 'src/app/impressions/domain/application-services/impressions-fascade.service';
import { IImpressionEntity } from 'src/app/impressions/domain/model/impressionEntity';
import { EmployeeInformationService } from '../../domain/infrastructure/employee-information.service';
import { IAppState } from 'src/app/common/app-state/app-state';
import { Observable } from 'rxjs';
import { Role } from 'src/app/common/app-state/role';
import { AppStateService } from 'src/app/common/app-state/app-state.service';

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

  public appState$: Observable<IAppState>;

  public doctorsMark?: number;


  constructor(private employeesService: EmployeesFascadeService, private impressionsService: ImpressionsFascadeService, private employeeInformationService: EmployeeInformationService, private appStateService: AppStateService) {

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
    this.appState$ = this.appStateService.getAppState();
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
      this.doctorsMark = Number(this.doctor.mark.toFixed(2));
  }

  public onDeleteDoctor(): void
  {

    if (this.doctor?.id == null || this.doctor.id.length == 0) {
      window.alert("You must select valid doctor!");
      return;
    }

    this.employeeInformationService.deleteDoctor(this.doctor.id)
    .subscribe((successfully: boolean) => {

      if (successfully)
      {
        window.alert("Doctor has been deleted.");
        window.location.reload();
      }
      else
        window.alert("An error occured. Please try later.");
    });
  }

  public isNurseLoggedIn(appState: IAppState): boolean {
    return appState.hasRole(Role.Nurse);
  }
}
