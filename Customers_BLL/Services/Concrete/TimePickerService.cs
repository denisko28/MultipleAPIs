using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;
using Customers_BLL.Services.Abstract;
using Customers_DAL.Entities;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;

namespace Customers_BLL.Services.Concrete
{
    public class TimePickerService : ITimePickerService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        private readonly IAppointmentRepository appointmentRepository;

        private readonly IPossibleTimeRepository possibleTimeRepository;

        private readonly IBarberRepository barberRepository;

        public TimePickerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            appointmentRepository = unitOfWork.AppointmentRepository;
            possibleTimeRepository = unitOfWork.PossibleTimeRepository;
            barberRepository = unitOfWork.BarberRepository;
        }

        // Only for testing !!!!!
        // public async Task<IEnumerable<TimeSpan>> GetPossibleTimeAsync()
        // {
        //     List<TimeSpan> possibleTime = new();
        //
        //     for (var h = 9; h <= 18; h++)
        //     {
        //         for (var m = 0; m <= 45; m += 15)
        //         {
        //             possibleTime.Add(new TimeSpan(h, m, 0));
        //         }
        //     }
        //
        //     return possibleTime;
        // }

        public async Task<IEnumerable<TimeResponse>> GetAvailableTimeAsync(int barberId, int duration, string dateStr)
        { 
            var availableTime = new List<TimeResponse>();
            var barbersDayOffs = await barberRepository.GetBarbersDayOffs(barberId);

            var date = DateTime.Parse(dateStr);
            if(barbersDayOffs.Any( dayOff => dayOff.Date.Equals(date)))
              return availableTime;
            
            var possibleTime = (await possibleTimeRepository.GetAllAvailableAsync())
              .Select(possibleTime => possibleTime.Time).ToList();
            
            var barbersAppointsForDate = (List<Appointment>) await appointmentRepository.GetAppointments(date, barberId);

            for (var i = 0; i < barbersAppointsForDate.Count;)
            {
              for (var j = 0; j < possibleTime.Count; j++)
              {
                if (possibleTime[j] <= barbersAppointsForDate[i].BeginTime ||
                    possibleTime[j] >= barbersAppointsForDate[i].EndTime)
                  continue;

                possibleTime.RemoveAt(j);
                j--;
              }
              barbersAppointsForDate.RemoveAt(i);
            }

            for (var i = 0; i < possibleTime.Count; i++)
            {
              var available = true;
              var stepSize = new TimeSpan(0, 15, 0);
              var steps = duration/15;
              for(var j = 0; (j < possibleTime.Count && j < steps); j++)
              {
                var expectedTime = possibleTime[i + j].Add(stepSize).ToString();
                if (possibleTime.Any(time => time.ToString() == expectedTime))
                  continue;
                available = false;
                break;
              }

              if (!available)
                continue;
              var beginTime = possibleTime[i];
              var endTime = possibleTime[i].Add(new TimeSpan(0, duration, 0));
              availableTime.Add(new TimeResponse(){ BeginTime = beginTime, EndTime = endTime });
            }

            return availableTime;
        }
    }
}
