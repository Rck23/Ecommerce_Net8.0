using System.Text;
using API.Ayudas;
using API.Ayudas.Errores;
using API.Servicios;
using AspNetCoreRateLimit;
using Core.Entidades;
using Core.Interfaces;
using Infraestructura.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensiones;

    public static class AppServiciosExtensiones
    {
        public static void ConfigurarCors(this IServiceCollection pinches_servicios){
            pinches_servicios.AddCors( opciones => 
                {
                    opciones.AddPolicy("CorsPolicy", siuu =>
                        siuu.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                    );
                }
            );
        }
        public static void AplicacionServicios(this IServiceCollection mas_servicios){
            mas_servicios.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>(); 

            mas_servicios.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
            mas_servicios.AddScoped<IUsuarioServicio, UsuarioServicio>();
        }

        public static void ConfiguracionApiVersion(this IServiceCollection servicios_bebe){
            servicios_bebe.AddApiVersioning( opciones => {
                opciones.DefaultApiVersion = new ApiVersion(1,0);
                opciones.AssumeDefaultVersionWhenUnspecified = true;

                opciones.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("version"),
                    new HeaderApiVersionReader("X-Version")
                ); 
                opciones.ReportApiVersions = true;
            });
        }

        public static void ConfiguracionPeticiones(this IServiceCollection servicios_servicios_y_mas_servicios){
            servicios_servicios_y_mas_servicios.AddMemoryCache();
            servicios_servicios_y_mas_servicios.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            servicios_servicios_y_mas_servicios.AddInMemoryRateLimiting();

            servicios_servicios_y_mas_servicios.Configure<IpRateLimitOptions>( opciones => {
                opciones.EnableEndpointRateLimiting = true; 
                opciones.StackBlockedRequests = false;
                opciones.HttpStatusCode = 429; 
                opciones.RealIpHeader = "X-Real-IP";
                opciones.GeneralRules = new List<RateLimitRule>{
                    new RateLimitRule{
                        Endpoint = "*",
                        Limit = 2,
                        Period = "10s"
                    }
                };
            });
        }

        public static void AddJwt(this IServiceCollection servicios_token, IConfiguration configuracion){
            servicios_token.Configure<JWT>(configuracion.GetSection("JWT"));
            servicios_token.AddAuthentication( opciones => {
                opciones.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opciones.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer( JwtBearer => {
                JwtBearer.RequireHttpsMetadata = false;
                JwtBearer.SaveToken = false;
                JwtBearer.TokenValidationParameters = new TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuracion["JWT:Editor"],
                    ValidAudience = configuracion["JWT:Audiencia"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuracion["JWT:Llave"]))
                };
            });
        }

        public static void AddValidacionErrores(this IServiceCollection servicios_validacion){
            servicios_validacion.Configure<ApiBehaviorOptions>( opciones => {
                opciones.InvalidModelStateResponseFactory = ActionContext => {
                    var errores  = ActionContext.ModelState.Where(u => u.Value.Errors.Count > 0)
                    .SelectMany(u => u.Value.Errors).Select(u => u.ErrorMessage).ToArray();
                    var erroresRespuesta  = new ApiValidacion() {
                        Errores = errores
                    }; 
                    return new BadRequestObjectResult(erroresRespuesta);
                };
            });
        }


    }
