using Microsoft.IdentityModel.Tokens;
using Mind_Master_Backend.DTOs;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mind_Master_Backend.Services
{
    public class TokenService
    {
        private IConfiguration _Configuration;

        public TokenService(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        public string GenerateJwt(AccountDTO account)
        {
            // Création de la signature du Jwt sur base d'une clef secret
            byte[] key = Encoding.UTF8.GetBytes(_Configuration["JwtOptions:Secret"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            // Création d'un objet de sécurité avec les données necessaire au token
            // Attention: Ne pas stocker d'information sensible !
            IEnumerable<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Login)
            };

            // Création du token
            // Alternative: Utiliser le "SecurityTokenDescriptor"
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _Configuration["JwtOptions:Issuer"],
                audience: _Configuration["JwtOptions:Audience"],
                expires: DateTime.Now.AddHours(_Configuration["JwtOptions:Expiration"] is null ? 1 : double.Parse(_Configuration["JwtOptions:Expiration"])),
                claims: claims,
                signingCredentials: credentials
            );

            // Génération sous forme de chaine de caractere 
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
