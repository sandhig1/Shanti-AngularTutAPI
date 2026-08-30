using System;
using Microsoft.Owin;
using Owin;

using System.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;

[assembly: OwinStartup(typeof(ERPTestAPI.Startup))]

namespace ERPTestAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureJwtAuthentication(app);
        }

        private void ConfigureJwtAuthentication(IAppBuilder app)
        {
            string jwtKey =
                ConfigurationManager
                    .AppSettings["config:JwtKey"];

            string issuer =
                ConfigurationManager
                    .AppSettings["config:JwtIssuer"];

            string audience =
                ConfigurationManager
                    .AppSettings["config:JwtAudience"];

            //string expiryDays = ConfigurationManager.AppSettings["config:JWTExpiryDays"];
            string expiryMins = ConfigurationManager.AppSettings["config:JWTExpiryMins"];

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            );

            var tokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = key,

                    ValidateIssuer = true,

                    ValidIssuer = issuer,

                    ValidateAudience = true,

                    ValidAudience = audience,

                    ValidateLifetime = true,

                    //ClockSkew =
                        //TimeSpan.FromDays(int.Parse(expiryDays))
                    ClockSkew =  TimeSpan.FromMinutes(int.Parse(expiryMins))
                };

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode =
                        AuthenticationMode.Active,

                    TokenValidationParameters =
                        tokenValidationParameters
                }
            );
        }
    }
}
