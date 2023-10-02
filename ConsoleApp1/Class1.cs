using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    public class TestJwtToken
    {
        public List<Claim> Claims { get; } = new();
        public int ExpiresInMinutes { get; set; } = 30;

        public TestJwtToken WithRole(string roleName)
        {
            Claims.Add(new Claim(ClaimTypes.Role, roleName));
            return this;
        }

        public TestJwtToken WithUserName(string username)
        {
            Claims.Add(new Claim(ClaimTypes.Upn, username));
            return this;
        }

        public TestJwtToken WithEmail(string email)
        {
            Claims.Add(new Claim(ClaimTypes.Email, email));
            return this;
        }

        public TestJwtToken WithDepartment(string department)
        {
            Claims.Add(new Claim("department", department));
            return this;
        }

        public TestJwtToken WithExpiration(int expiresInMinutes)
        {
            ExpiresInMinutes = expiresInMinutes;
            return this;
        }

        public string Build()
        {
            var path = Directory.GetCurrentDirectory() + "\\kit-test1.p12";
            X509Certificate2 cert = new X509Certificate2(path, "Test1234");

            var publicAndPrivate = cert.GetRSAPrivateKey;

            var token = new JwtSecurityToken(
                JwtTokenProvider.Issuer,
                JwtTokenProvider.Audience,
                Claims,
                expires: DateTime.Now.AddMinutes(ExpiresInMinutes),
                signingCredentials: new X509SigningCredentials(cert)
            );
            return JwtTokenProvider.JwtSecurityTokenHandler.WriteToken(token);
        }

        internal TestJwtToken WithClientId(string clientId)
        {
            Claims.Add(new Claim("clientId", clientId));
            return this;
        }

        internal TestJwtToken WithCvr(string v)
        {
            Claims.Add(new Claim("cvr", v));
            return this;
        }
    }

    public static class JwtTokenProvider
    {
        public static string Issuer { get; } = "http://localhost:8080/auth/realms/novax";
        public static string Audience { get; } = "organisation-api";
        internal static readonly JwtSecurityTokenHandler JwtSecurityTokenHandler = new();
    }
}
