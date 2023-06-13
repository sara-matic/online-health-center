import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { IAppState } from 'src/app/common/app-state/app-state';
import { EmployeeInformationService } from '../../domain/infrastructure/employee-information.service';
import { AppStateService } from 'src/app/common/app-state/app-state.service';
import { EmployeeInformationFascadeService } from '../../domain/application-services/employee-information-fascade.service';
import { Role } from 'src/app/common/app-state/role';

interface IAddDoctorFormData
{
  firstName: string;
  lastName: string;
  medicalSpecialty: string;
  title: string;
  biography: string;
}

@Component({
  selector: 'app-add-doctor-form',
  templateUrl: './add-doctor-form.component.html',
  styleUrls: ['./add-doctor-form.component.css']
})
export class AddDoctorFormComponent {

  public addDoctorForm: FormGroup;

  public appState$!: Observable<IAppState>;

  constructor(private employeeInformationService : EmployeeInformationFascadeService,
    private appStateService: AppStateService) {
    this.addDoctorForm = new FormGroup(
      {
        firstName: new FormControl(''),
        lastName: new FormControl(''),
        medicalSpecialty: new FormControl(''),
        title: new FormControl(''),
        biography: new FormControl('')
      }
    );
    this.appState$ = this.appStateService.getAppState();
  }

  public onAddDoctorSubmit(): void
  {
    const data: IAddDoctorFormData = this.addDoctorForm.value as IAddDoctorFormData;

    if (!this.pageDataIsValid(data)) {
      return;
    }

    const firstName = data.firstName;
    const lastName = data.lastName;
    const imageFile = data.firstName + "-" + data.lastName + ".jpg";
    const medicalSpecialty = data.medicalSpecialty;
    const title = data.title;
    const biography = data.biography;

    this.employeeInformationService.addDoctor(firstName, lastName, imageFile, medicalSpecialty, title, biography)
    .subscribe((errorMessage: string | null) => {
      if (errorMessage !== null) {
        window.alert(errorMessage);
      }
      else {
        window.alert('Doctor added successfully.');
        this.addDoctorForm.reset();
        window.location.reload();
      }
    });
    }
  
  private pageDataIsValid(data: IAddDoctorFormData): boolean
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
