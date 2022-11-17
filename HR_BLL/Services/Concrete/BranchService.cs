using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Events.BranchEvents;
using HR_BLL.DTO.Requests;
using HR_BLL.DTO.Responses;
using HR_BLL.Services.Abstract;
using HR_DAL.Entities;
using HR_DAL.Repositories.Abstract;
using HR_DAL.UnitOfWork.Abstract;
using MassTransit;

namespace HR_BLL.Services.Concrete
{
    public class BranchService : IBranchService
    {
        private readonly IMapper mapper;

        private readonly IBranchRepository branchRepository;
        
        private readonly IPublishEndpoint publishEndpoint;

        public BranchService(IUnitOfWork unitOfWork, IMapper mapper, IPublishEndpoint publishEndpoint) 
        {
            this.mapper = mapper;
            branchRepository = unitOfWork.BranchRepository;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<IEnumerable<BranchResponse>> GetAllAsync()
        {
            var results = await branchRepository.GetAllAsync();
            return results.Select(mapper.Map<Branch, BranchResponse>);
        }

        public async Task<BranchResponse> GetByIdAsync(int id)
        {
            var result = await branchRepository.GetByIdAsync(id);
            return mapper.Map<Branch, BranchResponse>(result);
        }

        public async Task<int> InsertAsync(BranchPostRequest request)
        {
            var entity = mapper.Map<BranchPostRequest, Branch>(request);
            var insertedId = await branchRepository.InsertAsync(entity);
            
            // send checkout event to rabbitmq
            var eventMessage = mapper.Map<BranchInsertedEvent>(entity);
            eventMessage.Id = insertedId;
            await publishEndpoint.Publish(eventMessage);
            
            return insertedId;
        }

        public async Task<bool> UpdateAsync(BranchRequest request)
        {
            var entity = mapper.Map<BranchRequest, Branch>(request);
            var result = await branchRepository.UpdateAsync(entity);
            
            var eventMessage = mapper.Map<BranchUpdatedEvent>(entity);
            await publishEndpoint.Publish(eventMessage);
            
            return result;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await branchRepository.DeleteByIdAsync(id);
            
            await publishEndpoint.Publish(new BranchDeletedEvent{Id = id});
        }
    }
}
