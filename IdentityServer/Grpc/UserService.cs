using AutoMapper;
using Grpc.Core;
using IdentityServer.Data.Abstract;
using IdentityServer.Exceptions;
using IdentityServer.Models;
using IdentityServer.Protos;

namespace IdentityServer.Grpc;

public class UserService : User.UserBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public override async Task<UserResponse> GetUserById(UserRequest request, ServerCallContext context)
    {
        try
        {
            var user = await _userRepository.GetUserById(request.UserId);
            return _mapper.Map<ApplicationUser, UserResponse>(user);
        }
        catch (EntityNotFoundException e)
        {
            throw new RpcException(new Status(StatusCode.NotFound, e.Message));
        }
        catch (Exception e)
        {
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }

    public override async Task<UsersResponse> GetUsersByIds(UsersRequest request, ServerCallContext context)
    {
        try
        {
            var usersResponse = new UsersResponse();
            var ids = request.Data.Select(data => data.UserId);
            var users = await _userRepository.GetUsersByIds(ids);
            var data = users.Select(_mapper.Map<ApplicationUser, UserResponse>);
            usersResponse.Data.AddRange(data);
            return usersResponse;
        }
        catch (Exception e)
        {
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }
}