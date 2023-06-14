import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IShowReportResponse } from '../../domain/models/show-report-response';
import { ReportsFacadeService } from '../../domain/application-services/reports-facade.service';
import { Observable, catchError } from 'rxjs';
import { IAppState } from 'src/app/common/app-state/app-state';
import { AppStateService } from 'src/app/common/app-state/app-state.service';
import { Role } from 'src/app/common/app-state/role';

@Component({
  selector: 'app-show-reports-form',
  templateUrl: './show-reports-form.component.html',
  styleUrls: ['./show-reports-form.component.css']
})
export class ShowReportsFormComponent {
  public filteredReports: IShowReportResponse[] = [];
  public doctorFilter: string = '';
  public patientFilter: string = '';
  public appState$: Observable<IAppState>;

  constructor(private appStateService: AppStateService, private reportsService: ReportsFacadeService) {
    this.appState$ = this.appStateService.getAppState();
    this.loadReports();  
  }

  private loadReports(): void {
    this.appState$.subscribe(appState => {
      if (appState.hasRole(Role.Patient)) {
        const patientId = appState.userId;
        this.reportsService.getReportsByPatientId(patientId as string).subscribe((reports: IShowReportResponse[]) => {
            this.filterReports(reports);
          },
          (error: any) => {
            window.alert('Failed to retrieve reports.');
            console.error('Failed to retrieve reports: ', error);
          }
        );
      } 
      else if (appState.hasRole(Role.Doctor)) {
        const doctorId = appState.userId;
        this.reportsService.getReportsByDoctorId(doctorId as string).subscribe((reports: IShowReportResponse[]) => {
          this.filterReports(reports);
        },
        (error: any) => {
          console.error('Failed to retrieve reports: ', error);
          window.alert('Failed to retrieve reports.');
        }
      );
      }
    })
  }

  public onDoctorFilterChange(): void {
    this.loadReports();
  }

  public onPatientFilterChange(): void {
    this.loadReports();
  }

  private filterReports(reports: IShowReportResponse[]): void {
    if (this.doctorFilter !== '') {
      //this.filteredReports = reports.filter(report => report.doctorId === this.doctorFilter);
      this.filteredReports = reports.filter(report => report.doctorFirstName.concat(report.doctorLastName).toLowerCase().includes(this.doctorFilter.toLowerCase())
        || report.doctorLastName.concat(report.doctorFirstName).toLowerCase().includes(this.doctorFilter.toLowerCase())
      );
    }
    else if (this.patientFilter !== '') {
      //this.filteredReports = reports.filter(report => report.patientId === this.patientFilter);
      this.filteredReports = reports.filter(report => report.patientFirstName.concat(report.patientLastName).toLowerCase().includes(this.patientFilter.toLowerCase())
        || report.patientLastName.concat(report.patientFirstName).toLowerCase().includes(this.patientFilter.toLowerCase())
      );
    } 
    else {
      this.filteredReports = reports;
    }
  }

  public isPatientLoggedIn(appState: IAppState): boolean {
    return appState.hasRole(Role.Patient);
  }

  public isDoctorLoggedIn(appState: IAppState): boolean {
    return appState.hasRole(Role.Doctor);
  }

  public isUserLoggedIn(appState: IAppState): boolean {
    return appState.userId !== undefined;
  }
}
