import { AppointmentRequestStatus, IAppointmentRequestStatus } from "./appointment-status";

export interface IAppointmentEntity
{
    doctorId: string;
    patientId: string;
    specialty: string;
    initialPrice: number;
    appointmentTime: Date;
    appointmentRequestStatus: IAppointmentRequestStatus;
    appointmentId: string;
    requestCreatedBy: string;
    requestCreatedTime: Date;
}