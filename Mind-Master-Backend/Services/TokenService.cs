using Microsoft.IdentityModel.Tokens;
using Mind_Master_Backend.DTOs;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mind_Master_Backend.Services
{
    /// <summary>Service responsable de génération d'un token</summary>
    public class TokenService
    {
        /// <summary>Instance de Jeu de clée/valeur venant de la configuration de l'appsetting</summary>
        private IConfiguration _Configuration;

        /// <summary>Constructeur</summary>
        /// <param name="configuration">Injection de la configuration des propriétés du projet</param>
        public TokenService(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        /// <summary>
        ///     Génére un token indifiant le compte
        ///     Ce token donne plus d'accés pour l'utilisateur
        /// </summary>
        /// <param name="account">Compte à encapsulé dans le token</param>
        /// <returns>Un JWT token pour le compte</returns>
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
