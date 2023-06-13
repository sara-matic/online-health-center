import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { IAppState } from 'src/app/common/app-state/app-state';
import { EmployeeInformationFascadeService } from '../../domain/application-services/employee-information-fascade.service';
import { AppStateService } from 'src/app/common/app-state/app-state.service';
import { IDoctorEntity } from 'src/app/common/domain/model/doctorEntity';
import { EmployeesFascadeService } from 'src/app/common/domain/application-services/employees-fascade.service';
import { Role } from 'src/app/common/app-state/role';

interface IUpdateDoctorFormData
{
  id: string;
  firstName: string;
  lastName: string;
  medicalSpecialty: string;
  title: string;
  biography: string;
}

@Component({
  selector: 'app-update-doctor-form',
  templateUrl: './update-doctor-form.component.html',
  styleUrls: ['./update-doctor-form.component.css']
})
export class UpdateDoctorFormComponent {

  public updateDoctorForm: FormGroup;

  public appState$!: Observable<IAppState>;

  public doctors: Array<IDoctorEntity> = this.getDoctors();

  public doctor? : IDoctorEntity;

  constructor(private employeeInformationService : EmployeeInformationFascadeService,
    private appStateService: AppStateService, private employeesService: EmployeesFascadeService) {
    this.updateDoctorForm = new FormGroup(
      {
        id: new FormControl(''),
        firstName: new FormControl(''),
        lastName: new FormControl(''),
        medicalSpecialty: new FormControl(''),
        title: new FormControl(''),
        biography: new FormControl('')
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

  public onUpdateDoctorSubmit(): void
  {
    const data: IUpdateDoctorFormData = this.updateDoctorForm.value as IUpdateDoctorFormData;

    if (!this.pageDataIsValid(data)) {
      return;
    }

    const selectedDoctor: IDoctorEntity = this.doctors.filter(doc => doc.id == data.id)[0];

    const firstName = data.firstName;
    const lastName = data.lastName;
    const imageFile = data.firstName + "-" + data.lastName + ".jpg";
    const medicalSpecialty = data.medicalSpecialty;
    const title = data.title;
    const biography = data.biography;

    this.employeeInformationService.updateDoctor(selectedDoctor.id, firstName, lastName, imageFile, medicalSpecialty, title, biography)
    .subscribe((errorMessage) => {
      if (errorMessage !== null) {
        window.alert(errorMessage);
      }
      else {
        window.alert('Doctor updated successfully.');
        this.updateDoctorForm.reset();
        window.location.reload();
      }
    });
    }
  
  private pageDataIsValid(data: IUpdateDoctorFormData): boolean
  {
    if (data.firstName == null || data.firstName.length == 0)
    {
      window.alert("Please fill in all the data.");
      return false;
    }

    if (data.lastName == null || data.lastName.length == 0)
    {
      window.alert("Please fill in all the data.");
      return false;
    }

    if (data.medicalSpecialty == null || data.medicalSpecialty.length == 0)
    {
      window.alert("Please fill in all the data.");
      return false;
    }

    if (data.title == null || data.title.length == 0)
    {
      window.alert("Please fill in all the data.");
      return false;
    }

    if (data.biography == null || data.biography.length == 0)
    {
      window.alert("Please fill in all the data.");
      return false;
    }

    return true;
  }
  
  public isNurseLoggedIn(appState: IAppState): boolean {
    return appState.hasRole(Role.Nurse);
  }


}
