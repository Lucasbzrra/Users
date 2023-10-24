using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UsuariosAPI.Authorization;
using UsuariosAPI.Data;
using UsuariosAPI.Models;
using UsuariosAPI.Service;

var builder = WebApplication.CreateBuilder(args);
//Antes
//var connstring = builder.Configuration.GetConnectionString("UsuarioConnection");
///Agora
var connstring = builder.Configuration["ConnectionStrings:UsuarioConnection"]; //<= Utilizaçao da Secret
// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddDbContext<UsuarioDbContext>(opt=>opt.UseMySql(builder.Configuration.GetConnectionString("UsuarioConnection"),ServerVersion.Autodetect(builder.Configuration.GetConnectionString("UsuarioConnection"));
builder.Services.AddDbContext<UsuarioDbContext>(opts =>
{
    opts.UseMySql(connstring, ServerVersion.AutoDetect(connstring)) ;
    
});


builder.Services.AddIdentity<Usuario, IdentityRole>().AddEntityFrameworkStores<UsuarioDbContext>().AddDefaultTokenProviders();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Fazendo a injeção nos construtores 
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();
// fazendo a injeção nos construtores ( AddSingleton: compartilha a mesma instância do serviço em toda a aplicação.)
builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

///Aplicando a politica de acesso
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("IdadeMinina", policy => policy.AddRequirements(new IdadeMinina(18)));
});



//Utilizando a biblioteca  para informar que estmaos passando um token na classe "IdadeAuthorization" da linha 13 até 23
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])), //<=Utilizaçao da Secret
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };

}
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// está ativando o middleware de autenticação na cadeia de middleware da sua aplicação.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
