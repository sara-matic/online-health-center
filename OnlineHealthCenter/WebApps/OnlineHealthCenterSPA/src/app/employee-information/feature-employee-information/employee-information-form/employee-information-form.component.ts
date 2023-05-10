import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

interface IEmployeeInformationFormData {
  id: string;
  firstName: string;
  lastName: string;
  title: string;
  specialty: string;
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

  public doctors: Array<IEmployeeInformationFormData> = [{id: "1", firstName: "Doctor", "lastName": "1", specialty: "Cardilology", title: "Specialist", biography: "Dr. Alan Stern was born in DuBois, Pennsylvania and is a graduate of Villanova University. He obtained his medical degree at Thomas Jefferson University in Philadelphia.", mark: 8},
  {id: "2", firstName: "Doctor", "lastName": "2", specialty: "Pulmology", title: "Specialist", biography: "Dr. David Sowa is an established and highly skilled physician with over 25 years of experience in obstetrics and gynecology. ", mark: 9},
  {id: "3", firstName: "Doctor", "lastName": "3", specialty: "Cardilology", title: "Primarius", biography: "Molly Nathanson MSN, CNM is originally from Massachusetts where she attended Brandeis University for her undergraduate degrees in Health.", mark: 10},
  {id: "4", firstName: "Doctor", "lastName": "4", specialty: "Cardilology", title: "Full Professor && Specialist", biography: "Pamela S. Haskins, CNM is originally from Springfield, MA. She received her nursing degree from Burbank Hospital School of Nursing in Fitchburg, MA", mark: 9}];

  public doctor? : IEmployeeInformationFormData;

  constructor() {

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

  public onSelectionChanged(): void
  {
      const data: IEmployeeInformationFormData = this.EmployeeInformationForm.value as IEmployeeInformationFormData;
      this.doctor = this.doctors.filter(d => d.id == data.id)[0]
      
  }

}
