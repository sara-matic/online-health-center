import { Injectable } from '@angular/core';
import { Observable, catchError, map, of, pairwise, switchMap, take } from 'rxjs';
import { AppStateService } from 'src/app/common/app-state/app-state.service';
import { EmployeeInformationService } from '../infrastructure/employee-information.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeInformationFascadeService {

  constructor(private employeeInformationService: EmployeeInformationService, private appStateService: AppStateService) { }

  public deleteDoctor(id: string): Observable<boolean>
  {
    return this.employeeInformationService.deleteDoctor(id);
  }
}
