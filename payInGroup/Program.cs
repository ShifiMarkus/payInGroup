using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using payInGroup;
using Repository.GeneratedModels;
using Services.DATA.CashData;
using Services.DATA.EmailData;
using Services.DATA.GroupsData;
using Services.DATA.PaymentData;
using Services.DATA.UsersData;
using Services.DATA.UsersInGroupData;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAppOrigin = "*";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAppOrigin,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000", "https://payingroupclient.onrender.com")
                          .AllowAnyHeader().AllowAnyMethod(); ;
                      });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cs = builder.Configuration["MyDBConnectionString"];
builder.Services.AddDbContext<MyDBContext>(options => options.UseNpgsql(cs));
builder.Services.AddScoped<IUsers, Users>();
builder.Services.AddScoped<IGroups, Groups>();
builder.Services.AddScoped<IUsersInGroups, UsersInGroups>();
builder.Services.AddScoped<ICashes, Cashes>();
builder.Services.AddScoped<IPayments, Payments>();
builder.Services.AddScoped<IEmail, Email>();


//mailkit
builder.Services.AddMailKit(optionBuilder =>
{
    optionBuilder.UseMailKit(new MailKitOptions()
    {
        //get options from sercets.json
        Server = builder.Configuration["Server"],
        Port = Convert.ToInt32(builder.Configuration["Port"]),
        SenderName = builder.Configuration["SenderName"],
        SenderEmail = builder.Configuration["SenderEmail"],
        // can be optional with no authentication 
        Account = builder.Configuration["Account"],
        Password = builder.Configuration["Password"],
        // enable ssl or tls
        Security = true
    });
});

//email manager
builder.Services.AddScoped<EmailManager>();

//goole storage manager - google cloud (upload images & files)
builder.Services.Configure<GoogleStorageManagerOptions>(builder.Configuration.GetSection("GoogleStorage"));
builder.Services.AddScoped<GoogleStorageManager>();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAppOrigin);
app.UseAuthorization();

app.MapControllers();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.Run();
