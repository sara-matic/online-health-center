import { Injectable } from '@angular/core';
import { RegistrationService } from '../infrastructure/registration.service';
import { Observable, map, take, of, catchError } from 'rxjs';
import { IRegistrationRequest } from '../model/registration-request';

@Injectable({
  providedIn: 'root'
})
export class RegistrationFacadeService {

  constructor(private registrationService: RegistrationService) { }

  public register(firstName: string, lastName: string, role: string, userName: string, password: string, email: string): Observable<string | null> {
    const request: IRegistrationRequest = {firstName, lastName, userName, password, email};

    return this.registrationService.register(request, role).pipe(
      take(1),
      map(response => {
        // no error ocured
        return null;
      }),
      catchError((err) => {
        let errorMessage: string;
        if (err.error && err.error.length !== 0) {
            errorMessage = Object.values(err.error).join('\n');
        }
        else {
          errorMessage = ('Registration is not successfull.');
        }
        console.error(err);
        return of(errorMessage);
      })
    );
  }
}