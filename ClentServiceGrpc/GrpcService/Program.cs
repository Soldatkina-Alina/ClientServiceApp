using Google.Protobuf.WellKnownTypes;
using GrpcService;
using GrpcService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

var service = new GreeterService(null);

//Тестовый вывод всех пользователей
var users = service.GetListUsers(new Google.Protobuf.WellKnownTypes.Empty(), null);
//Тестовое добавление
var addUser = service.AddUser(new UserReply
{
    FirstName = "AddUser",
}, null);

//Тестовое удаление одногот пользоваетля
//var deleteUser = service.DeleteUser(new DeleteRequest() {Id = 5}, null);
//Тестовое обновление
//var updateUser = service.UpdateUser(new UserReply {Id = 6, FirstName = "First",
//    Lastname = "LastName",
//Secondname = "S",
//Birthdaydate = Timestamp.FromDateTime(DateTime.UtcNow),
//Children = true
//}, null);
//var r = user;

//var rr = updateUser;

app.Run();
