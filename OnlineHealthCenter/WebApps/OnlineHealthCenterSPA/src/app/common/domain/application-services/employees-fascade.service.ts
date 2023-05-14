import { Injectable } from '@angular/core';
import { EmployeesService } from '../infrastructure/employees.service';
import { IDoctorEntity } from '../model/doctorEntity';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeesFascadeService {

  constructor(private employeesService: EmployeesService) { }

  public getDoctors(): Observable<Array<IDoctorEntity>> {
    return this.employeesService.getDoctors();
  }
}
