using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using System.Web.Http;
using System.Configuration;

using ERPTestAPI.Models;

using System.Web.Http.Cors;

namespace ERPTestAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpPost]
        public LoginReturnModel Login(LoginModel loginObj)
        {
            //Authentication _authentication = new Authentication();
            LoginReturnModel rtnObj = new LoginReturnModel();

            if (loginObj.UserName == Convert.ToString(ConfigurationManager.AppSettings["config:username"]) && loginObj.Password == Convert.ToString(ConfigurationManager.AppSettings["config:password"]))
            {
                var roles = new string[] { "SuperAdmin", "Admin" };
                var rolesList = roles.ToList();

                var JWTSecurityToken= Authentication.GenerateToken(loginObj.UserName, rolesList);

                if (JWTSecurityToken != null)
                {
                    rtnObj.status = true;
                    rtnObj.msg = "Login Succeeded";
                    rtnObj.token = JWTSecurityToken;
                }
                else {
                    rtnObj.status = false;
                    rtnObj.msg = "Error while authorizing";
                    rtnObj.token = "";
                }
            }
            else
            {
                rtnObj.status = false;
                rtnObj.msg = "Invalid Credential";
                rtnObj.token = "";
            }
                
            return rtnObj;
        }   //End public LoginReturnModel Login(LoginModel loginObj)

        /*[HttpPost]
        [Route("refresh")]
        public IHttpActionResult Refresh(RefreshTokenRequest model)
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

    }   //End public class LoginController : Controller

}   //End namespace ERPTestAPI.Controllers