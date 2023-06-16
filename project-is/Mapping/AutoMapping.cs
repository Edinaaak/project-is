using AutoMapper;
using BusLine.Contracts.Models.Bus.BusRequest;
using BusLine.Contracts.Models.Bus.Request;
using BusLine.Contracts.Models.Bus.Response;
using BusLine.Contracts.Models.Busline.Request;
using BusLine.Contracts.Models.Malfunction.Request;
using BusLine.Contracts.Models.Schedule;
using BusLine.Contracts.Models.Schedule.Request;
using BusLine.Contracts.Models.Ticket.Request;
using BusLine.Contracts.Models.Travel.Request;
using BusLine.Contracts.Models.User.Request;
using BusLine.Contracts.Models.User.Response;
using BusLine.Data.Models;
using BusLine.Infrastructure.Repositories;

namespace project_is.Mapping
{
    public class AutoMapping : Profile
    {

        public AutoMapping()
        {

            CreateMap<BusCreateRequest, Bus>();
            CreateMap<Bus, BusGetResponse>();
            CreateMap<BusUpdateRequest, Bus>();
            CreateMap<BuslineCreateRequest, BusLine.Data.Models.BusLine>();
            CreateMap<MalfunctionCreateRequest, Malfunction>();
            CreateMap<ScheduleCreateRequest, Schedule>();
            CreateMap<ScheduleUpdateRequest, Schedule>();
            CreateMap<ScheduleUserCreateRequest, ScheduleUser>();
            CreateMap<TicketCreateRequest, Ticket>();
            CreateMap<TravelCreateRequest, Travel>();
            CreateMap<User, UserResponse>();
            CreateMap<RegisterRequest, User>();
            CreateMap<User, UserResponse>();
            CreateMap<UserUpdateRequest, User>();

        }
    }
}
