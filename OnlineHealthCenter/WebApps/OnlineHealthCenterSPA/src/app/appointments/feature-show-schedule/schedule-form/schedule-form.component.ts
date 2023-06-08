import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AppointmentRequestStatus, RequestStatusEnum } from '../../domain/model/appointment-status';
import { AppointmentsFascadeService } from '../../domain/application-services/appointments-fascade.service';
import { IAppointmentEntity } from '../../domain/model/appointmentEntity';
import { DiscountsService } from '../../domain/infrastructure/discounts.service';
import { IDiscountEntity } from '../../domain/model/discountEntity';
import { AppStateService } from 'src/app/common/app-state/app-state.service';
import { IAppState } from 'src/app/common/app-state/app-state';
import { catchError, map, of, take } from 'rxjs';

interface IScheduleFormData {
  appointmentID: string;
  doctor: string;
  specialty: string;
  patientName: string;
  patientId: string;
  initialPrice: number;
  appointmentTime: string;
  appointmentTimeOriginalFormat: string;
  discount?: number;
  appointmentStatus?: string;
}

@Component({
  selector: 'app-schedule-form',
  templateUrl: './schedule-form.component.html',
  styleUrls: ['./schedule-form.component.css']
})
export class ScheduleFormComponent {
  public scheduleForm: FormGroup;
  public appointmentsCollection!: Array<IScheduleFormData>;
  public selectedAppointment?: IScheduleFormData;

  constructor(private service: AppointmentsFascadeService, private discountsSetvice: DiscountsService, 
    private appStateService: AppStateService) {
    this.scheduleForm = new FormGroup(
      {
        appointmentID: new FormControl(''),
        patientName: new FormControl(''),
        doctor: new FormControl(''),
        patientID: new FormControl(''),
        initialPrice: new FormControl(''),
        appointmentTime: new FormControl(''),
        discount: new FormControl(''),
        requestStatus: new FormControl('')
      }
    );

    this.appStateService.getAppState().pipe(
      take(1),
      map((appState: IAppState) => {
        return this.getAppointmentsByPatientId(appState.userId as string);
      }),
      catchError((err) => {
        window.alert('Failed to retrieve previous appointments.')
        console.error(err);
        return of([]);
      })
    ).subscribe((appointments: IScheduleFormData[]) => {
      this.appointmentsCollection = appointments;
    });
  }

  private getAppointmentsByPatientId(patientID: string): Array<IScheduleFormData> {
    this.service.getAppointmentsByPatientId(patientID).subscribe((appointments: Array<IAppointmentEntity>) => {
      this.appointmentsCollection = this.getFormDataFromAppointmentEntities(appointments);
    });

    this.populateDiscounts(patientID);

    return this.appointmentsCollection;
  }

  private populateDiscounts(patientID: string): void {
    this.discountsSetvice.getDiscountsByPatientId(patientID).subscribe(
      (discounts: Array<IDiscountEntity>) => {
        discounts.forEach(discount => {

          if (this.appointmentsCollection !== undefined)
            this.appointmentsCollection.forEach(apt => {  //appointmentsCollection je ovde undefined!!!
              if (apt.specialty == discount.specialty)
                apt.discount = discount.amountInPercentage;
            })
        });
      });
  }

  private getFormDataFromAppointmentEntities(entities: Array<IAppointmentEntity>): Array<IScheduleFormData> {
    var uiDataCollection = Array<IScheduleFormData>();

    entities.forEach(entity => {
      const appointmentUIData: IScheduleFormData = {
        appointmentID: entity.appointmentId,
        doctor: "dr "+ entity.doctorName + "-" + entity.specialty,
        patientName: entity.patientName,
        patientId: entity.patientId,
        initialPrice: entity.initialPrice,
        appointmentTime: new Date(entity.appointmentTime).toLocaleString(),
        appointmentTimeOriginalFormat: entity.appointmentTime.toString(),
        specialty: entity.specialty,
        discount: 0,
        appointmentStatus: new AppointmentRequestStatus(entity.appointmentRequestStatus.requestStatus).getRequestStatusDescription()
      }

      uiDataCollection.push(appointmentUIData);
    });

    return uiDataCollection;
  }

  public onSelectionChanged(): void {
    const data: IScheduleFormData = this.scheduleForm.value as IScheduleFormData;
    this.selectedAppointment = this.appointmentsCollection.filter(apt => apt.appointmentID === data.appointmentID)[0];

    const element = document.getElementById('applyDiscountButton');
    element!.hidden = this.selectedAppointment.discount === null || this.selectedAppointment.discount === 0;
  }

  public onAppointmentCancelationRequested(): void {

    if (this.selectedAppointment == null || this.selectedAppointment.appointmentID.length == 0) {
      window.alert("You must select valid appointment!");
      return;
    }

    if (window.confirm("Do you really want to cancel this appointment?\n\n Click OK to confirm or cancel to go back.")) {
      this.service.cancelAppointment(this.selectedAppointment.patientId, this.selectedAppointment.appointmentTimeOriginalFormat).subscribe(
        (successfully: boolean) => {
          
          if (successfully)
            window.alert("Appointment has been canceled!");
          else
            window.alert("Something went wrong.\nPlease try again.");

            window.location.reload();
        });
    }
  }

  public onDeleteAppointment(): void
  {
    if (this.selectedAppointment?.appointmentID == null || this.selectedAppointment.appointmentID.length == 0) {
      window.alert("You must select valid appointment!");
      return;
    }

    this.service.deleteDiscount(this.selectedAppointment.appointmentID)
      .subscribe((successfully: boolean) => {

        if (successfully)
        {
          window.alert("Appointment has been deleted.");
          window.location.reload();
        }
        else
          window.alert("An error occured. Please try later.");
      });
  }

  public onApplyDiscountRequested(): void {

    if (this.selectedAppointment == null || this.selectedAppointment.appointmentID.length == 0) {
      window.alert("You must select valid appointment!");
      return;
    }

    this.service.applyDiscount(this.selectedAppointment?.patientId, this.selectedAppointment?.specialty).subscribe(
      (successfully: boolean) => {
        
        if (successfully)
          window.alert("Discount has been applied successfully!");
        else
          window.alert("An error occurred. Please try later.");

        window.location.reload();
      });
  }

  public onApproveRequested(): void {
    if (this.selectedAppointment == null || this.selectedAppointment.appointmentTime.length == 0 || this.selectedAppointment.appointmentID.length == 0) {
      window.alert("You must select valid appointment!");
      return;
    }

    this.service.approveAppointment(this.selectedAppointment.patientId, this.selectedAppointment.appointmentTimeOriginalFormat).subscribe(
      (successfully: boolean) => {

        if (successfully)
          window.alert("Appointment has been approved!");
        else
          window.alert("Something went wrong.\nPlease try again.");

          window.location.reload();
      });
  }
}
