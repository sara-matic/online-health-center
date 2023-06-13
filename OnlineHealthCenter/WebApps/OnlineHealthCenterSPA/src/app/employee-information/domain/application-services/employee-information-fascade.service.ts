import { Injectable } from '@angular/core';
import { Observable, catchError, map, of, pairwise, switchMap, take } from 'rxjs';
import { AppStateService } from 'src/app/common/app-state/app-state.service';
import { EmployeeInformationService } from '../infrastructure/employee-information.service';
import { IAddDoctorRequest } from '../model/IAddDoctorRequest';
import { IUpdateDoctorRequest } from '../model/IUpdateDoctorRequest';

@Injectable({
  providedIn: 'root'
})
export class EmployeeInformationFascadeService {

  constructor(private employeeInformationService: EmployeeInformationService, private appStateService: AppStateService) { }

  public addDoctor(firstName: string, lastName: string, imageFile: string, medicalSpecialty: string, title: string, biography: string): Observable<Object> {
    
    const request: IAddDoctorRequest = { 
      firstName,
      lastName,
      imageFile,
      medicalSpecialty,
      title,
      biography
    };

    return this.employeeInformationService.addDoctor(request);
  }

  public updateDoctor(id: string, firstName: string, lastName: string, imageFile: string, medicalSpecialty: string, title: string, biography: string): Observable<Object> {
    const request: IUpdateDoctorRequest = {
      id,
      firstName,
      lastName,
      imageFile,
      medicalSpecialty,
      title,
      biography
    };

    return this.employeeInformationService.updateDoctor(request);
  }
  
  public deleteDoctor(id: string): Observable<boolean>
  {
    return this.employeeInformationService.deleteDoctor(id);
  }
}
