import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AppointmentsFascadeService } from 'src/app/appointments/domain/application-services/appointments-fascade.service';
import { EmployeesFascadeService } from 'src/app/common/domain/application-services/employees-fascade.service';
import { IApointmentTime } from 'src/app/appointments/domain/model/appointmentTimeEntity';
import { IDoctorEntity } from 'src/app/common/domain/model/doctorEntity';
import { HttpErrorResponse, withNoXsrfProtection } from '@angular/common/http';
import { AppStateService } from 'src/app/common/app-state/app-state.service';
import { Observable, catchError, map, of, take } from 'rxjs';
import { IAppState } from 'src/app/common/app-state/app-state';

interface IRequestFormData {
  doctor: string;
  patientID: string;
  patientName: string;
  initialPrice: number;
  appointmentDate: Date;
  appointmentTime: string;
}

@Component({
  selector: 'app-request-form',

  templateUrl: './request-form.component.html',
  styleUrls: ['./request-form.component.css']
})

export class RequestFormComponent {
  public requestForm: FormGroup;

  //Hard coded data:
  public doctors: Array<IDoctorEntity> = this.getDoctors();

  public readonly doctorTimes: Array<IApointmentTime> = [{ time: "8:00:00" }, { time: "9:00:00" }, { time: "10:00:00" }, { time: "11:00:00" }, { time: "12:00:00" }, { time: "13:00:00" },
  { time: "14:00:00" }, { time: "15:00:00" }, { time: "16:00:00" }];

  public appState$!: Observable<IAppState>;
  public initialPrice?: number;

  constructor(private employeesService: EmployeesFascadeService, private appointmentsService: AppointmentsFascadeService,
    private appStateService: AppStateService) {
    this.requestForm = new FormGroup(
      {
        doctor: new FormControl(''),
        patientID: new FormControl(''),
        patientName: new FormControl(''),
        initialPrice: new FormControl(''),
        appointmentDate: new FormControl(''),
        appointmentTime: new FormControl('')
      }
    );

    this.appState$ = this.appStateService.getAppState();
  }

  private getDoctors(): Array<IDoctorEntity> {
    this.employeesService.getDoctors().subscribe(
      (doctors: Array<IDoctorEntity>) => {
        this.doctors = doctors;
      });
      
    return this.doctors;
  }

  public onRequestSubmit(): void {
    const data: IRequestFormData = this.requestForm.value as IRequestFormData;

    if (!this.pageDataIsValid(data))
      return;

    const selectedDoctor: IDoctorEntity = this.doctors.filter(doc => doc.id == data.doctor)[0];

    if (window.confirm(
      "\nPatient ID: " + data.patientID + "\nPatient Name: " + data.patientName + "\nDoctor: " + selectedDoctor.firstName + " " + selectedDoctor.lastName + "\nAppointment date: " + new Date(data.appointmentDate).toLocaleDateString() + "\nAppointment time: " + data.appointmentTime + "\nInitial price: " + this.initialPrice
      + "\n\nClick OK to confirm request or Cancel it.")) 
    {
      const appointmentTime = data.appointmentDate + " " + data.appointmentTime;
      const selectedDoctorName = selectedDoctor.firstName + " " + selectedDoctor.lastName;

      this.appointmentsService.createAppointment(data.patientID, appointmentTime, selectedDoctor.medicalSpecialty, selectedDoctor.id, selectedDoctorName, data.patientName, "", this.initialPrice ?? 5000, appointmentTime, "WaitingForAnswer")
        .pipe(take(1),
          catchError((err) => {
            if (err instanceof HttpErrorResponse) {
              window.alert('Failed to create Appointment. \nPlease check your data validity.')
              console.error(err);
            }
            else
              window.alert('Failed to create Appointment. \nPlease try again later.')
            return of(false)
          }),
        ).subscribe((appointmentCreated: boolean) => 
        {
          if (appointmentCreated != null && appointmentCreated)
            window.alert("The appointment has been created successfully!");
        });
    }
  }

  public onSelectionChanged(): void {
    this.calculateInitialPrice();
  }

  private pageDataIsValid(data: IRequestFormData): boolean {
    if (data.doctor == null || data.doctor.length == 0) {
      window.alert("Please choose doctor in order to continue.");
      return false;
    }

    if (data.appointmentDate == null) {
      window.alert("Please select appointment date in order to continue.");
      return false;
    }

    if (data.appointmentTime == null || data.appointmentTime.length == 0) {
      window.alert("Please select appointment time in order to continue.");
      return false;
    }

    if (data.patientID == null || data.patientID.length == 0) {
      window.alert("Please insert valid patient ID in order to continue.");
      return false;
    }

    return true;
  }

  private getSelectedDoctor(): IDoctorEntity
  {
    const data: IRequestFormData = this.requestForm.value as IRequestFormData;
    return this.doctors.filter(doc => doc.id == data.doctor)[0] as IDoctorEntity;
  }

  private calculateInitialPrice(): void
  {
    const selectedDoctor = this.getSelectedDoctor();

    var price = 5000;

    if (selectedDoctor.title.includes("Specialist"))
      price += 500;
    
    if (selectedDoctor.title.includes("Prof"))
    {
      price += 1000;
    }

    this.initialPrice = price;
  }
}
