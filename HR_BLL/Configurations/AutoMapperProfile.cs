﻿using AutoMapper;
using MultipleAPIs.HR_DAL.Entities;
using MultipleAPIs.HR_BLL.DTO.Requests;
using MultipleAPIs.HR_BLL.DTO.Responses;
using MultipleAPIs.HR_BLL.Helpers;

namespace MultipleAPIs.HR_BLL.Configurations
{
    public class AutoMapperProfile : Profile
    {
        //private void CreateUserMaps()
        //{
        //    CreateMap<UserSignUpRequest, User>();
        //    CreateMap<UserRequest, User>();
        //    CreateMap<User, UserResponse>()
        //        .ForMember(
        //            response => response.FullName,
        //            options => options.MapFrom(user => $"{user.FirstName} {user.LastName}"))
        //        .ForMember(
        //            response => response.Avatar,
        //            options => options.MapFrom(
        //                user => !string.IsNullOrWhiteSpace(user.Avatar) ? $"Public/Photos/{user.Avatar}" : null));
        //}

        private void CreateBarberMaps()
        {
            CreateMap<BarberRequest, Barber>();
            CreateMap<Barber, BarberResponse>();

            CreateMap<Appointment, BarbersAppointmentsResponse>()
                .ForMember(
                    response => response.AppointmentId,
                    options => options.MapFrom(appointment => appointment.Id)
                )
                .ForMember(
                    response => response.AppointmentStatus,
                    options => options.MapFrom(appointment => AppointmentStatusHelper.GetStringStatus(appointment.AppointmentStatusId))
                );
        }

        private void CreateBranchMaps()
        {
            CreateMap<BranchRequest, Branch>();
            CreateMap<Branch, BranchResponse>();
        }

        private void CreateDayOffMaps()
        {
            CreateMap<DayOffRequest, DayOff>();
            CreateMap<DayOff, DayOffResponse>();
        }

        private void CreateEmployeeMaps()
        {
            CreateMap<EmployeeRequest, Employee>();
            CreateMap<Employee, EmployeeResponse>()
                .ForMember(
                    response => response.EmployeeStatus,
                    options => options.MapFrom(employee => EmployeeStatusHelper.GetStringStatus(employee.EmployeeStatusId))
                );
        }

        private void CreateEmployeeDayOffMaps()
        {
            CreateMap<EmployeeDayOffRequest, EmployeeDayOff>();
            CreateMap<EmployeeDayOff, EmployeeResponse>();
        }

        private void CreateEmployeeStatusMaps()
        {
            CreateMap<EmployeeStatusRequest, EmployeeStatus>();
            CreateMap<EmployeeStatus, EmployeeStatusResponse>();
        }

        public AutoMapperProfile()
        {
            CreateBarberMaps();
            CreateBranchMaps();
            CreateDayOffMaps();
            CreateEmployeeMaps();
            CreateEmployeeDayOffMaps();
            CreateEmployeeStatusMaps();
        }
    }
}