using JWT.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// === Dodaj serwis odpowiedzialny za autoryzacje tokenu
builder.Services.AddAuthentication().AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
    };
});
// ===

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// === Uruchom autoryzacje dla wyznaczonych koncowek
app.UseAuthorization();
// ===

app.MapControllers();

// === Przykladowy, prosty middleware
app.Use(async (context, next) =>
{
    Console.WriteLine("Hello world");
    await next(context);
    Console.WriteLine("Hello world 2");
});
// ===

// === Middleware wydzielony jako metoda rozszerzen
app.ConfigureExceptionHandler();


app.Run();