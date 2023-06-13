import { Injectable } from '@angular/core';
import { Observable, catchError, map, of, pairwise, switchMap, take } from 'rxjs';
import { AppStateService } from 'src/app/common/app-state/app-state.service';
import { EmployeeInformationService } from '../infrastructure/employee-information.service';
import { IAppState } from 'src/app/common/app-state/app-state';
import { IAddDoctorRequest } from '../model/IAddDoctorRequest';

@Injectable({
  providedIn: 'root'
})
export class EmployeeInformationFascadeService {

  constructor(private employeeInformationService: EmployeeInformationService, private appStateService: AppStateService) { }

  public addDoctor(firstName: string, lastName: string, imageFile: string, medicalSpecialty: string, title: string, biography: string): Observable<string | null> {
    return this.appStateService.getAppState().pipe(
      take(1),
      map((appState: IAppState) => {
        const request: IAddDoctorRequest = { 
          firstName,
          lastName,
          imageFile,
          medicalSpecialty,
          title,
          biography
        };

        return request;
      }),
      switchMap((request: IAddDoctorRequest) => {
        return this.employeeInformationService.addDoctor(request);
      }),
      map (() => {
        return null;
      }),
      catchError((err) => {
        let errorMessage: string;
        if (err.status === 403) {
          errorMessage = 'To add doctor you need to be logged in as a nurse.';
        }
        else {
          errorMessage = 'Failed to add doctor.';
        }
        console.error(err);
        return of(errorMessage);
      })
    );
  }
  
  public deleteDoctor(id: string): Observable<boolean>
  {
    return this.employeeInformationService.deleteDoctor(id);
  }
}
