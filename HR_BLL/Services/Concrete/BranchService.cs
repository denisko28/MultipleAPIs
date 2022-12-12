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

        public async Task<IEnumerable<BranchResponseDto>> GetAllAsync()
        {
            var results = await branchRepository.GetAllAsync();
            return results.Select(mapper.Map<Branch, BranchResponseDto>);
        }

        public async Task<BranchResponseDto> GetByIdAsync(int id)
        {
            var result = await branchRepository.GetByIdAsync(id);
            return mapper.Map<Branch, BranchResponseDto>(result);
        }

        public async Task<int> InsertAsync(BranchPostRequestDto requestDto)
        {
            var entity = mapper.Map<BranchPostRequestDto, Branch>(requestDto);
            var insertedId = await branchRepository.InsertAsync(entity);
            
            // send insert event to rabbitmq
            var eventMessage = mapper.Map<BranchInsertedEvent>(entity);
            eventMessage.Id = insertedId;
            await publishEndpoint.Publish(eventMessage);
            
            return insertedId;
        }

        public async Task<bool> UpdateAsync(BranchRequestDto requestDto)
        {
            var entity = mapper.Map<BranchRequestDto, Branch>(requestDto);
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
