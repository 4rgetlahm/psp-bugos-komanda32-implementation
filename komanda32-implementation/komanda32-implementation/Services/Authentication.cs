using komanda32_implementation.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace komanda32_implementation.Services
{
    public class Authentication
    {
        public static readonly Authentication Instance = new Authentication();
        private readonly SigningCredentials _signingCredentials;
        private readonly string Issuer;
        private readonly string Audience;
        private Authentication()
        {
            string JWTPrivateKey = Environment.GetEnvironmentVariable("JWT_KEY");
            Issuer = Environment.GetEnvironmentVariable("ISSUER") ?? "";
            Audience = Environment.GetEnvironmentVariable("AUDIENCE") ?? "";
            if(JWTPrivateKey == null)
            {
                throw new ArgumentException(JWTPrivateKey);
            }
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTPrivateKey));
            _signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        }

        public string GetHash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }

        public string GenerateJSONWebToken(IAuthenticatable authenticatable)
        {
            Claim[] claims =
            {
                new Claim(JwtRegisteredClaimNames.Sub, authenticatable.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, authenticatable.Email)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                Issuer, 
                Audience, 
                claims,
                expires: DateTime.UtcNow.AddMinutes(180), 
                signingCredentials: _signingCredentials
            ); 

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
