import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatAutocomplete } from '@angular/material/autocomplete';
import { Observable, catchError, filter, of, switchMap } from 'rxjs';
import { UserFacadeService } from 'src/app/identity/domain/application-services/user-facade.service';
import { IUserDetails } from 'src/app/identity/domain/model/user-details';
import { ReportsFacadeService } from '../../domain/application-services/reports-facade.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-create-report-form',
  templateUrl: './create-report-form.component.html',
  styleUrls: ['./create-report-form.component.css']
})
export class CreateReportFormComponent {
  public createReportForm: FormGroup;
  public patientControl: FormControl = new FormControl('', [Validators.required]);
  filteredPatients: Observable<IUserDetails[]>;

  constructor(private userService: UserFacadeService, private reportsService: ReportsFacadeService) {
    this.createReportForm = new FormGroup({
      patientControl: this.patientControl,
      commentControl: new FormControl('', [Validators.required, Validators.minLength(1)]),
      prescriptionControl: new FormControl('', [Validators.required, Validators.minLength(1)]),
      diagnosisControl: new FormControl('', [Validators.required, Validators.minLength(1)])
    });
    this.filteredPatients = this.patientControl.valueChanges.pipe(
      filter(name => name?.length > 0),
      switchMap((name: string) => this.filterPatients(name)),
      catchError((err) => {
        if (err instanceof HttpErrorResponse) {
          if (err.status === 401) {
            window.alert('Please login.');
          }
          else if (err.status === 403) {
            window.alert('Please login as a doctor.');
          }
        }
        else {
          console.error(err);
          window.alert('An error occurred.');
        }
        this.patientControl.setValue('');
        return of([]);
      })
    );
  }

  private filterPatients(name: string): Observable<IUserDetails[]> {
    return this.userService.searchUsersByName(name);
  }

  onCreateReportFormSubmit(): void {
    if (this.createReportForm.invalid) {
      window.alert('You must fill in the blanks.');
      return;
    }

    const selectedPatient: IUserDetails = this.patientControl.value as IUserDetails;

    if (!selectedPatient || !selectedPatient.id) {
      window.alert('Please select a valid patient.');
      return;
    }

    const comment: string = this.createReportForm.controls['commentControl'].value;
    const prescription: string = this.createReportForm.controls['prescriptionControl'].value;
    const diagnosis: string = this.createReportForm.controls['diagnosisControl'].value;

    this.reportsService
      .createReport(selectedPatient.id, selectedPatient.firstName, selectedPatient.lastName, comment, diagnosis, prescription)
      .subscribe((errorMessage: string | null) => {
        if (errorMessage !== null) {
          window.alert(errorMessage);
        }
        else {
          window.alert('Report created successfully.');
          this.createReportForm.reset();
        }
      });
  }

  displayPatient(patient: IUserDetails): string {
    if (patient?.id) {
      return `${patient.firstName} ${patient.lastName} (email: ${patient.email}, id: ${patient.id})`;
    }
    return '';
  }
}
