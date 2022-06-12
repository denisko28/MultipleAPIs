using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Customers_DAL.Entities;
using Customers_DAL.Repositories.Abstract;
using Customers_DAL.UnitOfWork.Abstract;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;
using Customers_BLL.Exceptions;
using Customers_BLL.Helpers;
using Customers_BLL.Services.Abstract;
using Customers_DAL.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Customers_BLL.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        private readonly IUserRepository userRepository;
        
        private readonly IImageService imageService;
        
        private readonly UserManager<User> userManager;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            userRepository = unitOfWork.UserRepository;
            this.imageService = imageService;
            userManager = unitOfWork.UserManager;
        }

        public async Task<IEnumerable<UserResponse>> GetAllAsync()
        {
            var users = await userRepository.GetAllAsync();
            var responses = new List<UserResponse>();
            foreach (var user in users)
            {
                var response = mapper.Map<User, UserResponse>(user);
                var roles = await userManager.GetRolesAsync(user);
                response.Role = roles.FirstOrDefault();
                responses.Add(response);
            }
            return responses;
        }
        
        public async Task<UserResponse> GetByIdAsync(int id, UserClaimsModel userClaims)
        {
            if(userClaims.Role != UserRoles.Admin && userClaims.UserId != id)
                throw new ForbiddenAccessException($"You don't have access to user info with id: {id}");
            
            var user = await userRepository.GetByIdAsync(id);
            var response = mapper.Map<User, UserResponse>(user);
            var roles = await userManager.GetRolesAsync(user);
            response.Role = roles.FirstOrDefault();
            return response;
        }

        public async Task SetAvatarForUserAsync(ImageUploadRequest request, UserClaimsModel userClaims)
        {
            if(userClaims.Role != UserRoles.Admin && userClaims.UserId != request.Id)
                throw new ForbiddenAccessException($"You don't have access to edit avatar of user with id: {request.Id}");
            
            var user = await userRepository.GetByIdAsync(request.Id);
            user.Avatar = await imageService.SaveImageAsync(request.Image);
            await userRepository.UpdateAsync(user);
            await unitOfWork.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            var user = await userRepository.GetByIdAsync(id);

            await userManager.DeleteAsync(user);
        }
    }
}
