using Backend.Data;
using Backend.Services.Users;
using Backend.Services.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Cors
ConfigureCors(builder);

// Deploy no IIS
ConfigureIIS(builder);

// Configurações do contexto de banco de dados usando o provedor SQL Server
ConfigureDB(builder);

// Configurações de autenticação JWT
ConfigureJWT(builder);

// Configurações do Swagger
ConfigureSwagger(builder);

// Configurações de autorização
builder.Services.AddAuthorization();

// Adiciona a configuração de controladores ao serviço
builder.Services.AddControllers();

// Adiciona os serviços de escopo para injeção de dependência
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<TasksService>();

// Constrói a aplicação
var app = builder.Build();

// Habilita o CORS usando a política "AllowAllOrigins"
app.UseCors("AllowAllOrigins");

app.UseSwagger();
app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "remind API v1");
    options.RoutePrefix = "";
});

app.UseAuthentication(); // Adiciona a autenticação à pipeline da aplicação
app.UseAuthorization(); // Adiciona a autorização à pipeline da aplicação

app.MapControllers(); // Mapeia os controladores da aplicação

app.Run(); // Executa a aplicação

void ConfigureCors(WebApplicationBuilder builder) {
    builder.Services.AddCors(options => {
        options.AddPolicy("AllowAllOrigins", builder => {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    });
}

void ConfigureIIS(WebApplicationBuilder builder) {
    builder.Services.Configure<IISServerOptions>(options => {
        options.AutomaticAuthentication = false;
    });
}

void ConfigureDB(WebApplicationBuilder builder) {
    builder.Services.AddDbContext<ApplicationDbContext>(options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
}

void ConfigureJWT(WebApplicationBuilder builder) {
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateAudience = false, // Não valida a audiência do token
            ValidateIssuer = false, // Não valida o emissor do token
            ValidateLifetime = true, // Valida a expiração do token
            ValidateIssuerSigningKey = true, // Valida a chave de assinatura do token
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:Key"])) // Chave de assinatura do token
        };
    });
}

void ConfigureSwagger(WebApplicationBuilder builder) {
    builder.Services.AddSwaggerGen(options => {

        // Configuração do documento Swagger
        options.SwaggerDoc("v1", new OpenApiInfo {
            Title = "re.mind API",
            Version = "v1",
            Description = "A simple API that mixes Kanban with todoList for people with Attention Deficit Hyperactivity Disorder - TDAH",
            Contact = new OpenApiContact {
                Name = "Anália, Geismar e Jessica",
                Email = "contato@remind.com.br",
                Url = new Uri("https://www.remind.com/br")
            }
        });

        // Definição do esquema de segurança "Bearer"
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
            Description = "Please enter your JWT token",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer"
        });

        // Adição do requisito de segurança "Bearer" para todas as operações
        options.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });
}