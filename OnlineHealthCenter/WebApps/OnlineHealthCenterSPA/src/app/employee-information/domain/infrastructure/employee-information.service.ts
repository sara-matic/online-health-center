import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeInformationService {

  constructor(private httpClient: HttpClient) { }

  private readonly commonPath = "http://localhost:8000/api/v1/Doctor/";

  public deleteDoctor(id: string): Observable<boolean>
  {
    const connectionString = this.commonPath + 'DeleteDoctor/' + id;
    return this.httpClient.delete<boolean>(connectionString);
  }
}
