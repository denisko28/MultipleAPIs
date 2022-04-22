﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using HR_BLL.Services.Abstract;
using HR_DAL.Entities;
using HR_DAL.Repositories.Abstract;
using HR_DAL.UnitOfWork.Abstract;

namespace HR_BLL.Services.Concrete
{
    public class DayOffService : IDayOffService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        private readonly IDayOffRepository dayOffRepository;

        public DayOffService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            dayOffRepository = unitOfWork.DayOffRepository;
        }

        public async Task<IEnumerable<DayOffResponse>> GetAllAsync()
        {
            var results = await dayOffRepository.GetAllAsync();
            return results.Select(mapper.Map<DayOff, DayOffResponse>);
        }

        public async Task<DayOffResponse> GetByIdAsync(int id)
        {
            var result = await dayOffRepository.GetByIdAsync(id);
            return mapper.Map<DayOff, DayOffResponse>(result);
        }

        public async Task<int> InsertAsync(DayOffRequest request)
        {
            var entity = mapper.Map<DayOffRequest, DayOff>(request);
            var result = await dayOffRepository.InsertAsync(entity);
            unitOfWork.Commit();
            return result;
        }

        public async Task<bool> UpdateAsync(DayOffRequest request)
        {
            var entity = mapper.Map<DayOffRequest, DayOff>(request);
            var result = await dayOffRepository.UpdateAsync(entity);
            unitOfWork.Commit();
            return result;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await dayOffRepository.DeleteByIdAsync(id);
            unitOfWork.Commit();
        }
    }
}
