using BaseContext;
using BaseHandler.Handlers.Conrete;
using BaseHandler.Repository.Implementation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService;
using Microsoft.EntityFrameworkCore.Storage;

namespace GrpcService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// ��������� ���� ������������� �� �����
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<ListUserReply> GetAllUserFromFile(GetAllUserFromFieleRequest request, ServerCallContext context)
        {
            ListUserReply listUserReply = new ListUserReply();

            var module = new ReadUserFromJson(new UserRepository(), request.Path);
            var usersList = module.ReadAllEntityFromFile().Select(item => new UserReply {
                Id = ((User)item).Id, 
                FirstName = ((User)item).Firstname, 
                Secondname = ((User)item)?.Secondname ?? "", 
                Lastname = ((User)item)?.Lastname ?? "",
                Birthdaydate = ((User)item).Birthdaydate != null ? Timestamp.FromDateTimeOffset(((User)item).Birthdaydate.Value.ToDateTime(new TimeOnly())) : null, 
                Children = ((User)item).Children});
            listUserReply.Users.AddRange(usersList);

            return Task.FromResult(listUserReply);
        }

        /// <summary>
        /// ��������� ���� �������������
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<ListUserReply> GetListUsers(Empty request, ServerCallContext context)
        {
            ListUserReply listUserReply = new ListUserReply();
            var module = new UserDataHandler(new UserRepository());
            
            var listUsers = module.GetAllUsers().ToList<User>();

            listUserReply.Users.AddRange(listUsers.Select(item => new UserReply
            {
                Id = item.Id,
                FirstName = item.Firstname,
                Secondname = item.Secondname ?? string.Empty,
                Lastname = item.Lastname ?? string.Empty,
                Birthdaydate = (item.Birthdaydate == DateOnly.MinValue || item.Birthdaydate == null) ? null : Timestamp.FromDateTimeOffset(new DateTime(item.Birthdaydate.Value.Year, item.Birthdaydate.Value.Month, item.Birthdaydate.Value.Day)), //Timestamp.FromDateTimeOffset(DateTime.Now),
                Children = item.Children
            }));

            return Task.FromResult(listUserReply);
        }

        /// <summary>
        /// ���������� ������������ �� UI
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<CreateUserReply> AddUser(UserReply request, ServerCallContext context)
        {
            var module = new UserDataHandler(new UserRepository());
            var result = module.Add(new User
            {
                Firstname = request.FirstName,
                Secondname = request.Secondname ?? null,
                Lastname = request.Lastname ?? null,
                Birthdaydate = request.Birthdaydate == null ? null : DateOnly.FromDateTime(request.Birthdaydate.ToDateTime()),
                Children = request.Children ?? null
            });

            return Task.FromResult(new CreateUserReply { Succes = result}); 
        }

        /// <summary>
        /// ����� ������������ �� Id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<UserReply> GetUserById(GetUserRequest request, ServerCallContext context)
        {
            var module = new UserDataHandler(new UserRepository());
            var user = module.GetUserById(request.Id);

            return Task.FromResult(new UserReply
            {
                Id = user.Id,
                FirstName = user.Firstname,
                Secondname = user.Secondname ?? string.Empty,
                Lastname = user.Lastname ?? string.Empty,
                Birthdaydate = (user.Birthdaydate == DateOnly.MinValue || user.Birthdaydate == null) ? null : Timestamp.FromDateTimeOffset(new DateTime(user.Birthdaydate.Value.Year, user.Birthdaydate.Value.Month, user.Birthdaydate.Value.Day)),
                Children = user.Children
            });
        }

        /// <summary>
        /// ���������� ������������� �� �����
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<CreateUserReply> AddUserFromFile(GetAllUserFromFieleRequest request, ServerCallContext context)
        {
            var module = new ReadUserFromJson(new UserRepository(), request.Path);

            return Task.FromResult(new CreateUserReply { Succes = module.AddEntity() });
        }

        /// <summary>
        /// �������� ������������
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<DeleteUserReply> DeleteUser(DeleteRequest request, ServerCallContext context)
        {
            var module = new UserDataHandler(new UserRepository());

            return Task.FromResult(new DeleteUserReply { Succes = module.Remove(request.Id) });
        }

        /// <summary>
        /// ���������� ������������
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<CreateUserReply> UpdateUser(UserReply request, ServerCallContext context)
        {
            var module = new UserDataHandler(new UserRepository());
            DateOnly d = DateOnly.FromDateTime(request.Birthdaydate.ToDateTime());

            var result = module.Update(new User
            {
                Id = request.Id,
                Firstname = request.FirstName,
                Secondname = request.Secondname,
                Lastname = request.Lastname,
                Birthdaydate = DateOnly.FromDateTime(request.Birthdaydate.ToDateTime()),
                Children = request.Children
            });

            return Task.FromResult(new CreateUserReply { Succes = result });
        }
    }
}