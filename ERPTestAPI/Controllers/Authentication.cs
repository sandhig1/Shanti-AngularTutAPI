using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Claims;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;

namespace ERPTestAPI
{
    public class Authentication
    {
        public static string GenerateToken(string username, List<string> roles)
        {
            var jwtKey =
                ConfigurationManager
                    .AppSettings["config:JWTKey"];

            var jwtIssuer =
                ConfigurationManager
                    .AppSettings["config:JWTIssuer"];

            var jwtAudience =
                ConfigurationManager
                    .AppSettings["config:JWTAudience"];

            //var expiryDays =
            //    Convert.ToDouble(
            //        ConfigurationManager
            //            .AppSettings["config:JWTExpiryDays"]
            //    );

            var expiryMins =
                Convert.ToDouble(
                    ConfigurationManager
                        .AppSettings["config:JWTExpiryMins"]
                );

            var claims = new List<Claim>
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    username
                ),

                new Claim(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString()
                ),

                new Claim(
                    ClaimTypes.NameIdentifier,
                    username
                ),

                new Claim(
                    ClaimTypes.Name,
                    username
                )
            };

            if (roles != null)
            {
                foreach (var role in roles)
                {
                    claims.Add(
                        new Claim(
                            ClaimTypes.Role,
                            role
                        )
                    );
                }
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            //var expires =
            //    DateTime.UtcNow.AddDays(expiryDays);

            var expires = DateTime.UtcNow.AddMinutes(expiryMins);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }   //End public static string GenerateToken(string username, List<string> roles)

        /*public IHttpActionResult Refresh(RefreshTokenRequest model)
        {
            if (model == null ||
                string.IsNullOrWhiteSpace(model.RefreshToken))
            {
                return BadRequest(
                    "Refresh token is required."
                );
            }

            // Hash token received from Angular
            var tokenHash =
                RefreshTokenHelper.HashToken(
                    model.RefreshToken
                );

            // Find refresh token in database
            var storedToken =
                GetRefreshToken(tokenHash);

            if (storedToken == null)
            {
                return Unauthorized();
            }

            // Check revoked
            if (storedToken.IsRevoked)
            {
                return Unauthorized();
            }

            // Check expiry
            if (storedToken.ExpiresAt <= DateTime.UtcNow)
            {
                return Unauthorized();
            }

            // Get user
            var user =
                GetUserById(storedToken.UserId);

            if (user == null)
            {
                return Unauthorized();
            }

            // Get latest roles
            var roles =
                GetUserRoles(user.UserId);

            // Generate NEW access token
            var newAccessToken =
                JwtHelper.GenerateAccessToken(
                    user.UserName,
                    roles
                );

            // Generate NEW refresh token
            var newRefreshToken =
                RefreshTokenHelper.GenerateRefreshToken();

            var newRefreshTokenHash =
                RefreshTokenHelper.HashToken(
                    newRefreshToken
                );

            var expiryDays =
                Convert.ToDouble(
                    ConfigurationManager
                        .AppSettings["config:RefreshTokenExpiryDays"]
                );

            // Revoke old refresh token
            RevokeRefreshToken(
                storedToken.RefreshTokenId,
                newRefreshTokenHash
            );

            // Save new refresh token
            SaveRefreshToken(
                user.UserId,
                newRefreshTokenHash,
                DateTime.UtcNow.AddDays(expiryDays)
            );

            return Ok(new TokenResponse
            {
                AccessToken = newAccessToken,

                RefreshToken = newRefreshToken,

                ExpiresIn = 30 * 60,

                TokenType = "Bearer",

                UserId = user.UserId.ToString(),

                UserName = user.UserName
            });
        }*/

    }   //End public class Authentication

}   //End namespace ERPTestAPI