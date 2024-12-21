using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcSharedProtoFile;
using Infrastructure.RepositoryPattern;
using Mapster;
using Model.Entities;

namespace GrpcService.Services
{
    public class UserServiceInApp : UserService.UserServiceBase
    {
        private readonly IBaseRepository<User, Guid> _userRepository;
        public UserServiceInApp(IBaseRepository<User, Guid> userRepository)
        {
            _userRepository = userRepository;
        }
        public override async Task<UserResponse> CreateUser(UserRequest request, ServerCallContext context)
        {
            var user = request.Adapt<User>();
            user.Id = Guid.NewGuid();
            var result = await _userRepository.CreateDataAsync(user);
            var response = new UserResponse() { User = result.Adapt<UserRequest>(), Message = "User created" };
            return response;
        }

        public override async Task<UserResponse> ReadUser(UserIdRequest request, ServerCallContext context)
        {
            var user = await _userRepository.GetByIdAsync(Guid.Parse(request.Id));
            if (user != null)
            {
                return new UserResponse { Message = "User found", User = user.Adapt<UserRequest>() };
            }
            return new UserResponse { Message = "User not found" };
        }

        public override async Task<UserResponse> UpdateUser(UserRequest request, ServerCallContext context)
        {
            var userData = await _userRepository.GetAsync(x => x.Id == Guid.Parse(request.Id));
            if (userData != null)
            {
                var mapData = request.Adapt<User>();
                userData.FirstName = mapData.FirstName;
                userData.LastName = mapData.LastName;
                userData.DateOfBirth = mapData.DateOfBirth;
                userData.NationalCode = mapData.NationalCode;

                await _userRepository.UpdateDataAsync(userData);
                return new UserResponse { Message = "User updated", User = request };
            }
            return new UserResponse { Message = "User not found" };
        }

        public override async Task<UserResponse> DeleteUser(UserIdRequest request, ServerCallContext context)
        {
            var userData = await _userRepository.GetAsync(x => x.Id == Guid.Parse(request.Id));
            if (userData != null)
            {
                await _userRepository.DeleteDataAsync(userData);
                return new UserResponse { Message = "User deleted", User = userData.Adapt<UserRequest>() };
            }
            return new UserResponse { Message = "User not found" };
        }

        public override async Task<UsersListResponse> ListUsers(Empty request, ServerCallContext context)
        {
            var users = (await _userRepository.GetAllAsync()).ToList();
            var response = new UsersListResponse();
            response.Users.AddRange(users.Select(x => x.Adapt<UserRequest>()).ToList());
            return response;
        }
    }
}
