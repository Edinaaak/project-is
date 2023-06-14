using AutoMapper;
using BusLine.Contracts.Models.Bus.BusRequest;
using BusLine.Contracts.Models.Bus.Request;
using BusLine.Contracts.Models.Bus.Response;
using BusLine.Contracts.Models.Busline.Request;
using BusLine.Contracts.Models.Malfunction.Request;
using BusLine.Contracts.Models.Schedule.Request;
using BusLine.Data.Models;

namespace project_is.Mapping
{
    public class AutoMapping : Profile
    {
        
        public AutoMapping() {

            CreateMap<BusCreateRequest, Bus>();
            CreateMap<Bus, BusGetResponse>();
            CreateMap<BusUpdateRequest, Bus>();
            CreateMap<BuslineCreateRequest, BusLine.Data.Models.BusLine>();
            CreateMap<MalfunctionCreateRequest, Malfunction>();
            CreateMap<ScheduleCreateRequest, Schedule>();
        }

    }
}
