﻿using Appointments.Application.Common.DTOs;
using Appointments.Application.Persistance;
using Appointments.Domain.Aggregates;
using Dapper;
using Appointments.Domain.Enums;
using Appointments.Domain.Utilities;

namespace Appointments.Infrastructure.Repositories
{
    internal class AppointmentsRepository : IAppointmentRepository
    {
        private readonly IAppointmentContext context;

        public AppointmentsRepository(IAppointmentContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> ApproveAppointment(ApproveAppointmentDTO approveAppointmentDTO)
        {
            using (var connecton = this.context.GetConnection())
            {
                string appointmentTime = AppointmentsTimeUtility.GetCommonDateTimeFormat(approveAppointmentDTO.AppointmentTime);

                var appointment = await connecton.QueryFirstOrDefaultAsync<Appointment>("SELECT * FROM Appointments WHERE PatientId = @PatientId AND AppointmentTime = @AppointmentTime", new { PatientId = approveAppointmentDTO.PatientId, AppointmentTime = appointmentTime });

                if (appointment == null)
                    return false;

                appointment.ChangeAppointmentRequestStatus(RequestStatusEnum.Approved);

                int rowsAffected = await connecton.ExecuteAsync("UPDATE Appointments SET AppointmentRequestStatus = @AppointmentRequestStatus WHERE PatientId = @PatientId AND AppointmentTime = @AppointmentTime",
                    new { AppointmentRequestStatus = appointment.AppointmentRequestStatus.RequestStatus.ToString(), PatientId = approveAppointmentDTO.PatientId, AppointmentTime = appointmentTime });

                return rowsAffected > 0;
            }
        }

        public async Task<bool> ApproveAppointment(string appointmentId)
        {
            using (var connecton = this.context.GetConnection())
            {
                var appointment = await connecton.QueryFirstOrDefaultAsync<Appointment>("SELECT * FROM Appointments WHERE AppointmentId = @AppointmentId", new { AppointmentId = appointmentId});

                if (appointment == null)
                    return false;

                appointment.ChangeAppointmentRequestStatus(RequestStatusEnum.Approved);

                int rowsAffected = await connecton.ExecuteAsync("UPDATE Appointments SET AppointmentRequestStatus = @AppointmentRequestStatus WHERE AppointmentId = @AppointmentId",
                    new { AppointmentRequestStatus = appointment.AppointmentRequestStatus.RequestStatus.ToString(), appointmentId = appointmentId});

                return rowsAffected > 0;
            }
        }


        public async Task<bool> CancelAppointment(string appointmentId)
        {
            using (var connecton = this.context.GetConnection())
            {
                var appointment = await connecton.QueryFirstOrDefaultAsync<Appointment>("SELECT * FROM Appointments WHERE AppointmentId = @AppointmentId", new { AppointmentId = appointmentId });

                if (appointment == null)
                    return false;

                appointment.ChangeAppointmentRequestStatus(RequestStatusEnum.Canceled);

                int rowsAffected = await connecton.ExecuteAsync("UPDATE Appointments SET AppointmentRequestStatus = @AppointmentRequestStatus WHERE AppointmentId = @AppointmentId",
                    new { AppointmentRequestStatus = appointment.AppointmentRequestStatus.RequestStatus.ToString(), appointmentId = appointmentId });

                return rowsAffected > 0;
            }
        }

        public async Task<bool> CancelAppointment(CancelAppointmentDTO cancelAppointmentDTO)
        {
            using (var connecton = this.context.GetConnection())
            {
                string appointmentTime = AppointmentsTimeUtility.GetCommonDateTimeFormat(cancelAppointmentDTO.AppointmentTime);

                var appointment = await connecton.QueryFirstOrDefaultAsync<Appointment>("SELECT * FROM Appointments WHERE PatientId = @PatientId AND AppointmentTime = @AppointmentTime", new { PatientId = cancelAppointmentDTO.PatientId, appointmentTime });

                if (appointment == null)
                    return false;

                appointment.ChangeAppointmentRequestStatus(RequestStatusEnum.Canceled);

                int rowsAffected = await connecton.ExecuteAsync("UPDATE Appointments SET AppointmentRequestStatus = @AppointmentRequestStatus WHERE PatientId = @PatientId AND AppointmentTime = @AppointmentTime",
                    new { AppointmentRequestStatus = appointment.AppointmentRequestStatus.RequestStatus.ToString(), PatientId = cancelAppointmentDTO.PatientId, AppointmentTime = appointmentTime });

                return rowsAffected > 0;
            }
        }

        public async Task<bool> CreateAppointment(CreateAppointmentDTO createAppointmentDTO)
        {
            int rowsAffected = 0;

            using (var connecton = this.context.GetConnection())
            {
                 rowsAffected = await connecton
                    .ExecuteAsync("INSERT INTO Appointments" +
                    "(AppointmentId, DoctorId, PatientId, DoctorName, PatientName, Specialty, AppointmentTime, AppointmentRequestStatus, InitialPrice, RequestCreatedBy, RequestCreatedTime)" +
                    "VALUES(@AppointmentId, @DoctorId, @PatientId,@DoctorName, @PatientName, @Specialty, @AppointmentTime, @AppointmentRequestStatus, @InitialPrice, @RequestCreatedBy, @RequestCreatedTime)",
                    new
                    {
                        AppointmentId = createAppointmentDTO.AppointmentId,
                        DoctorId = createAppointmentDTO.DoctorId,
                        PatientId = createAppointmentDTO.PatientId,
                        DoctorName = createAppointmentDTO.DoctorName,
                        PatientName = createAppointmentDTO.PatientName,
                        Specialty = createAppointmentDTO.Specialty,
                        AppointmentTime = AppointmentsTimeUtility.GetCommonDateTimeFormat(createAppointmentDTO.AppointmentTime),
                        InitialPrice = createAppointmentDTO.InitialPrice,
                        RequestCreatedBy = createAppointmentDTO.RequestCreatedBy,
                        RequestCreatedTime = AppointmentsTimeUtility.GetCommonDateTimeFormat(createAppointmentDTO.RequestCreatedTime),
                        AppointmentRequestStatus = createAppointmentDTO.RequestStatus.ToString()
                    });
            }

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAppointment(string appointmentId)
        {
            using (var connection = this.context.GetConnection())
            {
                int affectedRows = await connection.ExecuteAsync("DELETE FROM Appointments WHERE AppointmentId=@AppointmentId", new { AppointmentId = appointmentId });
                return affectedRows > 0;
            }
        }

        public async Task<Appointment> GetAppointmentByTime(string patientId, string appointmentTime)
        {
            string preconfiguredAppointmentTime = AppointmentsTimeUtility.GetCommonDateTimeFormat(appointmentTime);

            using (var connection = this.context.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Appointment>("SELECT * FROM Appointments WHERE PatientId = @PatientId AND AppointmentTime = @AppointmentTime", new { PatientId = patientId, AppointmentTime = preconfiguredAppointmentTime });
            }
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorId(string doctorId)
        {
            using (var connection = this.context.GetConnection())
            {
                return await connection
                    .QueryAsync<Appointment>("SELECT * FROM Appointments WHERE DoctorId = @DoctorId", new { DoctorId = doctorId });
            }
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientId(string patientId)
        {
            using (var connection = this.context.GetConnection())
            {
                return await connection
                    .QueryAsync<Appointment>("SELECT * FROM Appointments WHERE PatientId = @PatientId", new { PatientId = patientId });
            }
        }

        public async Task<bool> CheckCreateAppointmentRequestValidity(CreateAppointmentDTO createAppointmentDTO)
        {
            using (var connection = this.context.GetConnection())
            {
                string appointmentTime = AppointmentsTimeUtility.GetCommonDateTimeFormat(createAppointmentDTO.AppointmentTime);

                var rowsCount = await connection
                    .QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Appointments " +
                    "WHERE (PatientId = @PatientId AND AppointmentRequestStatus = 'Approved' AND AppointmentTime = @AppointmentTime)" +
                        " OR (DoctorId = @DoctorId AND AppointmentRequestStatus = 'Approved' AND AppointmentTime = @AppointmentTime)",
                    new { PatientId = createAppointmentDTO.PatientId, DoctorId = createAppointmentDTO.DoctorId, AppointmentTime = appointmentTime });

                return rowsCount == 0;
            }
        }

        public async Task<int?> ApplyDiscount(ApplyAppointmentDiscountDTO applyAppointmentDiscountDTO)
        {
            using (var connection = this.context.GetConnection())
            {
                var appointment = await connection
                 .QueryFirstAsync<Appointment>("SELECT * FROM Appointments WHERE PatientId = @PatientId AND Specialty = @Specialty",
                 new { PatientId = applyAppointmentDiscountDTO.PatientId, Specialty = applyAppointmentDiscountDTO.Specialty });

                appointment.ApplyDiscount(applyAppointmentDiscountDTO.AmountInPercentage);

                int rowsAffected = await connection.ExecuteAsync("UPDATE Appointments SET InitialPrice = @InitialPrice WHERE PatientId = @PatientId AND Specialty = @Specialty",
                   new { PatientId = applyAppointmentDiscountDTO.PatientId, Specialty = appointment.Specialty, InitialPrice = appointment.InitialPrice });

                return appointment.InitialPrice;
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointments()
        {
            using (var connection = this.context.GetConnection())
            {
                return await connection.QueryAsync<Appointment>("SELECT * FROM Appointments");
            }
        }
    }
}
