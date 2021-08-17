using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using RIEGO.Client;
using RIEGO.Client.Repositorios;
using RIEGO.Shared.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Blazored.SessionStorage;

namespace RIEGO.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IConfiguration Configuration;
        TokenValidationParameters _tokenvalidationParameters;
        public Usuario usr;
        public List<Permisos> permisosDelUsr;
        public List<string> permisosDelUsuario;
        private readonly ApplicationDBContext _context;

        public AuthController(ApplicationDBContext context, IConfiguration Config, Blazored.SessionStorage.ISessionStorageService _sessionStorage)
        {
            _context = context;
            this.Configuration = Config;
        }

        [HttpGet("login")]
        public IActionResult Login(string user, string pass)
        {
            var passant = pass;
            pass = Client.Utility.Encrypt(pass);
            string tokenstring = GenerateToken(user, pass);
            return Ok(tokenstring);
        }

        private async Task GetUserFromDB(string user, string pass)
        {
            var usuario = _context.Usuario.Where(x => x.Activo == true).ToList();
            usr = usuario.Where(x => x.UserName == user && x.Password == pass).FirstOrDefault();
            if (usr != null)
            {
                var permisos = _context.Permisos.Where(x => x.Activo == true).ToList();
                permisosDelUsr = permisos.Where(x => x.IdRol == usr.IdRol && x.Valor == true).ToList();
                permisosDelUsuario = permisosDelUsr.Select(x => x.Descripcion).ToList();
            }
        }

        [HttpGet("logout")]
        public IActionResult Logout(string token, string secret)
        {
            secret = Configuration["AuthSettings:key"];
            var tokenHandler = new JwtSecurityTokenHandler();
            string newToken = "";
            try
            {
                newToken = ""; // se valida el token principal
                _tokenvalidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters();
                _tokenvalidationParameters.ValidateIssuerSigningKey = true;
                _tokenvalidationParameters.ValidateIssuer = true;
                _tokenvalidationParameters.ValidateAudience = true;
                _tokenvalidationParameters.ValidAudience = Configuration["AuthSettings:Audince"];
                _tokenvalidationParameters.ValidIssuer = Configuration["AuthSettings:Issuer"];
                _tokenvalidationParameters.RequireExpirationTime = true;
                _tokenvalidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:key"]));

                var pricipal = tokenHandler.ValidateToken(token, _tokenvalidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgoritm(validatedToken))
                {
                    return null;
                }
                // usar clases especificas
                var expiryDate = DateTime.Now.Ticks;
                var DateTimeExpire = DateTime.Now;
                var exp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDate);//.Subtract(_tokenvalidationParameters.);
                if (exp > DateTime.UtcNow) //aun no expira
                { }
                else // expiro , volver a generarlo 
                {
                    var user = pricipal.Claims.Single(o => o.Type == "User").Value;
                    newToken = GenerateToken(user, "", secret);
                }
            }
            catch (Exception e)
            { }
            return Ok(newToken);
        }

        private string GenerateToken(string user, string pass, string secret = "")
        {
            List<Claim> claims = new List<Claim>();
            string tokenAsString = "";

            GetUserFromDB(user, pass).ConfigureAwait(true);

            if (((usr != null && !usr.EsAdmin && permisosDelUsr.Count > 0) || (usr != null && usr.EsAdmin)) || (usr != null && secret == Configuration["AuthSettings:key"]))
            {
                if (usr.EsAdmin)
                {
                    claims.Add(new Claim("User", usr.UserName));
                    claims.Add(new Claim("Rol", "usuario"));
                    claims.Add(new Claim("EsAdmin", usr.EsAdmin.ToString()));
                    claims.Add(new Claim("IdEmpresa", usr.IdEmpresa.ToString()));
                    claims.Add(new Claim("IdSucursal", usr.IdSucursal.ToString()));
                    //foreach (var item in permisosDelUsuario) //un admin tiene todos los permisos so no importa /***/
                    //{
                    //    claims.Add(new Claim("Permisos", item)); /***/
                    //}
                    claims.Add(new Claim(ClaimTypes.Role, "Administrador"));
                    claims.Add(new Claim("VerReportes", "xWeSoyAdmin"));
                }
                else
                {
                    claims.Add(new Claim("User", usr.UserName));
                    claims.Add(new Claim("Rol", "usuario"));
                    claims.Add(new Claim("EsAdmin", usr.EsAdmin.ToString()));
                    claims.Add(new Claim("IdEmpresa", usr.IdEmpresa.ToString()));
                    claims.Add(new Claim("IdSucursal", usr.IdSucursal.ToString()));
                    claims.Add(new Claim(ClaimTypes.Role, "Usuario"));
                    claims.Add(new Claim("VerReportes", "False"));

                    foreach (var item in permisosDelUsuario)
                    {
                        if (item == "VerReportes")/***/
                        {
                            claims.RemoveAt(6); // quitar el falsex
                            claims.Add(new Claim("VerReportes", "True")); //poner en true
                            break;
                        }
                        else { }
                    }

                    foreach (var item in permisosDelUsuario)
                    {
                        if (item == "VerReportes")/***/
                        { }
                        else
                            claims.Add(new Claim("Permisos" + item, item));
                    }
                }
                IdentityModelEventSource.ShowPII = true;
                var keybuffer = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:key"]));
                DateTime expireTime = DateTime.Now.AddSeconds(28800); //expira // cambio aqui para debugear
                var token = new JwtSecurityToken(issuer: Configuration["AuthSettings:Issuer"], audience: Configuration["AuthSettings:Audince"], claims, expires: expireTime, signingCredentials: new SigningCredentials(keybuffer, SecurityAlgorithms.HmacSha256));
                tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
            }
            return tokenAsString;
        }

        [HttpGet("Refresh")]
        public IActionResult Refresh(string token, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string newToken = "";
            try
            {
                newToken = "";  // se valida el token principal
                _tokenvalidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["AuthSettings:Audince"],// valida de donde viene el token y si es corrento lo usa 
                    ValidIssuer = Configuration["AuthSettings:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:key"])),
                };
                var pricipal = tokenHandler.ValidateToken(token, _tokenvalidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgoritm(validatedToken))
                {
                    return null;
                }
                // usar clases especificas
                var expiryDate = long.Parse(pricipal.Claims.Single(o => o.Type == JwtRegisteredClaimNames.Exp).Value);
                var DateTimeExpire = new DateTime(expiryDate);
                var exp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDate);//.Subtract(_tokenvalidationParameters.);
                if (exp > DateTime.UtcNow)  //aun no expira
                { }
                else // expiro , volver a generarlo 
                {
                    var user = pricipal.Claims.Single(o => o.Type == "User").Value;
                    newToken = GenerateToken(user, "", secret);
                }
            }
            catch (Exception e)
            { }
            return Ok(newToken);
        }
        private bool IsJwtWithValidSecurityAlgoritm(SecurityToken token)
        {
            return (token is JwtSecurityToken jwtSecurotyToken) && jwtSecurotyToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture);
        }
    }
}
