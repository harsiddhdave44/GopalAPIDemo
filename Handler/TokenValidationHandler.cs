using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GopalAPIDemo.Handler
{
    public class TokenValidationHandler:DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //    HttpStatusCode statusCode;
            //    string token;

            //    if (!TryRetrieveToken(request, out token))
            //    {
            //        return base.SendAsync(request, cancellationToken);
            //    }
            //    //If the token is found, then the following code is to be executed!

            //    try
            //    {
            //        string secret = Configuration.AppSettings["Secret"];
            //        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            //        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            //        SecurityToken validatedToken;
            //        TokenValidationParameters validationParameters = new TokenValidationParameters()
            //        {
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ValidateAudience = false,
            //            ValidateIssuer = false,
            //            IssuerSigningKey = securityKey,
            //        };

            //        Thread.CurrentPrincipal = handler.ValidateToken(token, validationParameters, out validatedToken);
            //        HttpContext.Current.User = handler.ValidateToken(token, validationParameters, out validatedToken);

            //        return base.SendAsync(request, cancellationToken);
            //    }
            //    catch (SecurityTokenValidationException)
            //    {
            //        statusCode = HttpStatusCode.Unauthorized;
            //    }
            //    catch (Exception)
            //    {
            //        statusCode = HttpStatusCode.InternalServerError;
            //    }

            return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage() { });
        }
    }
}
