using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using BusinessEntities;
using SecurityServices;

namespace StreamApi
{
    public class AuthenticationModule
    {
        private const string CommunicationKey = "GQDstc21ewfffffffffffFiwDffVvVBrk";
        readonly SecurityKey _signingKey = new InMemorySymmetricSecurityKey(Encoding.UTF8.GetBytes(CommunicationKey));

        private string _validationError = string.Empty;


        public string GetLastValidationError()
        {
            return _validationError;
        }
        // The Method is used to generate token for user
        public string GenerateTokenForUser(UserSecurityModel userSecurity, string[] roles)
        {
            var signingKey = new InMemorySymmetricSecurityKey(Encoding.UTF8.GetBytes(CommunicationKey));
            var now = DateTime.UtcNow;
            var signingCredentials = new SigningCredentials(signingKey,
               SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);

            var fullName = string.Empty;
            if (userSecurity.User.Contact != null)
            {
                fullName = userSecurity.User.Contact.FirstName + " " + userSecurity.User.Contact.LastName;
            }
            var claimsIdentity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userSecurity.User.UserName),
                new Claim(ClaimTypes.NameIdentifier, userSecurity.User.Id.ToString()),
                new Claim(ClaimTypes.GivenName, fullName)
                //new Claim(ClaimTypes.Role, "All"),
                //new Claim(ClaimTypes.Role, "Admin")
            }, "Custom");

            foreach (string role in roles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                AppliesToAddress = "http://www.streamline.com",
                TokenIssuerName = "self",
                Subject = claimsIdentity,
                SigningCredentials = signingCredentials,
                Lifetime = new Lifetime(now, now.AddMinutes(int.Parse(InternalSettings.TokenExpirationMinutes))),
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var plainToken = tokenHandler.CreateToken(securityTokenDescriptor);
            // var plainToken = tokenHandler.CreateToken(securityTokenDescriptor);
            var signedAndEncodedToken = tokenHandler.WriteToken(plainToken);
          
            return signedAndEncodedToken;

        }

        /// Using the same key used for signing token, user payload is generated back
        public JwtSecurityToken GenerateUserClaimFromJwt(string authToken)
        {

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidAudiences = new string[]
                      {
                    "http://www.streamline.com",
                      },

                ValidIssuers = new string[]
                  {
                      "self",
                  },
                IssuerSigningKey = _signingKey
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken validatedToken;

            try
            {

                tokenHandler.ValidateToken(authToken, tokenValidationParameters, out validatedToken);
            }
            catch (SecurityTokenValidationException ex)
            {
                Debug.WriteLine(ex.Message);
                _validationError = ex.Message.Split(new[] {"\r\n", "\r", "\n"}, StringSplitOptions.None)[0];
                return null;
            }
            catch (SignatureVerificationFailedException sEx)
            {
                _validationError = sEx.Message.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)[0];
                return null;
            }

            return validatedToken as JwtSecurityToken;

        }

        public JwtAuthenticationIdentity PopulateUserIdentity(JwtSecurityToken userPayloadToken)
        {
            string fullName = ((userPayloadToken)).Claims.FirstOrDefault(m => m.Type == "given_name").Value;

            string name = ((userPayloadToken)).Claims.FirstOrDefault(m => m.Type == "unique_name").Value;
            string userId = ((userPayloadToken)).Claims.FirstOrDefault(m => m.Type == "nameid").Value;

            List<Claim> roles = userPayloadToken.Claims.Where(m => m.Type == "role").ToList();
            var jwtAuthIdentiy = new JwtAuthenticationIdentity(name) { UserId = Convert.ToInt32(userId), UserName = name, FullName = fullName};
            
            foreach (var role in roles)
            {
                jwtAuthIdentiy.Roles.Add(role.Value);
                jwtAuthIdentiy.AddClaim(new Claim(ClaimTypes.Role, role.Value));
            }
            
            return jwtAuthIdentiy;  // new JwtAuthenticationIdentity(name) { UserId = Convert.ToInt32(userId), UserName = name };

        }
    }
}