using System;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using RestWithASPNETUdemy.Configurations;

namespace RestWithASPNETUdemy.Services.Implementations
{
    public class TokenService : ITokenService
    {

        // Injetar a classe de configuração do Token
        private TokenConfiguration _configuration;

        public TokenService(TokenConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Gerar Token com JWT
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            //Seta secretKey e gera uma chave simetrica baseado na configuração do appsettings.json
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));

            //Gerar o siginCredentials
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //Gerar as otions
            var options = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_configuration.Minutes),
                signingCredentials: signinCredentials
            );

            //Gera token
            string tokenString = new JwtSecurityTokenHandler().WriteToken(options);
            return tokenString;
        }

        // Método responsável por gerar o RefreshToken 
        public string GenerateRefreshToken()
        {
            // Gerar números aleatórios
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            };
        }

        //Método expiração Token
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters{
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
           
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            
            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCulture))

                //Mensagem de exeção
                throw new SecurityTokenException("Invalid Token"); 

            return principal;
        }
    }
}
