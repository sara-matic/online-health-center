export enum RequestStatusEnum
{
  WaitingForAnswer = 1,
  Approved = 2,
  Canceled = 3,
  Rejected = 4
}

export interface IAppointmentRequestStatus
{
  requestStatus?: RequestStatusEnum;
}

export class AppointmentRequestStatus implements IAppointmentRequestStatus
{
  requestStatus?: RequestStatusEnum;

  constructor(requestStatus?: RequestStatusEnum)
  {
    this.requestStatus = requestStatus;
  }

  public getRequestStatusDescription(): string
  {
    switch(this.requestStatus)
    {
      case RequestStatusEnum.Approved:
        return "Approved";
      case RequestStatusEnum.Canceled:
        return "Canceled";
      case RequestStatusEnum.Rejected:
        return "Rejected";
      default:
        return "Waiting for answer";
    }
  } 
}