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
        /// Получение всех пользователей из файла
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
        /// Получение всех пользователей
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<ListUserReply> GetListUsers(Empty request, ServerCallContext context)
        {
            ListUserReply listUserReply = new ListUserReply();
            var module = new UserDataHandler(new UserRepository());
            
            var listUsers = module.GetAllUsers().ToList<User>();

            var listUsersReply = listUsers.Select(item => new UserReply
            {
                Id = item.Id,
                FirstName = item.Firstname,
                Secondname = item.Secondname,
                Lastname = item.Lastname,
                Birthdaydate = (item.Birthdaydate == DateOnly.MinValue || item.Birthdaydate == null) ? null : Timestamp.FromDateTimeOffset(new DateTime(item.Birthdaydate.Value.Year, item.Birthdaydate.Value.Month, item.Birthdaydate.Value.Day)), //Timestamp.FromDateTimeOffset(DateTime.Now),
                Children = item.Children
            });

            listUserReply.Users.AddRange(listUsers.Select(item => new UserReply
            {
                Id = item.Id,
                FirstName = item.Firstname,
                Secondname = item.Secondname,
                Lastname = item.Lastname,
                Birthdaydate = (item.Birthdaydate == DateOnly.MinValue || item.Birthdaydate == null) ? null : Timestamp.FromDateTimeOffset(new DateTime(item.Birthdaydate.Value.Year, item.Birthdaydate.Value.Month, item.Birthdaydate.Value.Day)), //Timestamp.FromDateTimeOffset(DateTime.Now),
                Children = item.Children
            }));

            return Task.FromResult(listUserReply);
        }

        /// <summary>
        /// Добавление пользователя из UI
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<CreateUserReply> AddUser(UserReply request, ServerCallContext context)
        {
            var module = new UserDataHandler(new UserRepository());

            return Task.FromResult(new CreateUserReply { Succes = module.Add(new User { Firstname = request.FirstName}) }); 
        }

        /// <summary>
        /// Добавление пользователей из файла
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<CreateUserReply> AddUserFromFile(GetAllUserFromFieleRequest request, ServerCallContext context)
        {
            var module = new ReadUserFromJson(new UserRepository(), request.Path);

            return Task.FromResult(new CreateUserReply { Succes = module.AddEntity() });
        }
    }
}