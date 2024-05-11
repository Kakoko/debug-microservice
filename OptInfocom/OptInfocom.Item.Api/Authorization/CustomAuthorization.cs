using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace OptInfocom.Item.Api.Authorization
{
    public class ApiAuthorize : Attribute, IAuthorizationFilter
    {
        //private readonly ErpDbContext _context;
        //private readonly ILoggerManager _logger;
        private string _appKey;

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {

            if (filterContext != null)
            {
                Microsoft.Extensions.Primitives.StringValues authTokens;
                Microsoft.Extensions.Primitives.StringValues AppID;
                Microsoft.Extensions.Primitives.StringValues AppKey;

                filterContext.HttpContext.Request.Headers.TryGetValue("authorization", out authTokens);
                filterContext.HttpContext.Request.Headers.TryGetValue("appid", out AppID);
                filterContext.HttpContext.Request.Headers.TryGetValue("appkey", out AppKey);

                var _token = authTokens.FirstOrDefault();
                var _appid = AppID.FirstOrDefault();
                _appKey = AppKey.FirstOrDefault();

                // Store appkey in HttpContext.Items
                if (_appKey != null)
                {
                    filterContext.HttpContext.Items["appkey"] = _appKey;
                }

                if (_token != null)
                {
                    string authToken = _token;
                    if (authToken != null)
                    {
                        if (!IsValidToken(authToken))
                        {
                            filterContext.HttpContext.Response.Headers.Add("Authorization", authToken);
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");

                            filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");

                            return;
                        }
                        else
                        {
                            filterContext.HttpContext.Response.Headers.Add("Authorization", authToken);
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                            filterContext.Result = new JsonResult("NotAuthorized")
                            {
                                Value = new
                                {
                                    Status = "Error",
                                    Message = "Invalid Token"
                                },
                            };
                        }

                    }

                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please Provide authToken";
                    filterContext.Result = new JsonResult("Please Provide authToken")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = "Please Provide authToken"
                        },
                    };
                }
            }
        }

        public bool IsValidToken(string authToken)
        {
            bool IsValid = false;
            try
            {
                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://api.erp9i.com/api/auth/login",
                    ValidAudience = "http://api.erp9i.com",
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"))
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appKey))
                };

                SecurityToken validatedToken;
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                authToken = authToken.Replace("Bearer", "").Replace("Basic", "").Trim();
                var user = handler.ValidateToken(authToken, validationParameters, out validatedToken);
                IsValid = user.Identity.IsAuthenticated;
            }
            catch (Exception ex)
            {
                IsValid = false;
            }
            return IsValid;
        }
    }
}
