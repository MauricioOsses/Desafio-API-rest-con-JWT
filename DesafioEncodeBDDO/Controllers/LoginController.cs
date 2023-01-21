using DesafioEncodeBDDO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DesafioEncodeBDDO.Services;
using DesafioEncodeBDDO.Models.Dto;
using AutoMapper;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace DesafioEncodeBDDO.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly ApplicationContext _dbContext;
        private readonly LoginService _LoginService;
        private IConfiguration _iconfig;


        public LoginController(ApplicationContext dbContext, LoginService loginService, IConfiguration iconfig)
        {
            _dbContext = dbContext;
            _LoginService = loginService;
            _iconfig = iconfig;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Login(UserDto user)
        {
            var usr = await _LoginService.GetUser(user);

            if (usr == null)
            {
                return BadRequest("Credenciales invalidas...");

            }
            // generar token
            string JwToken = GenerateToken(usr);
            return Ok(new { token = JwToken });
        }
        private string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.NameUser),
                new Claim("AdminType", user.Rol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfig.GetSection("Jwt:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }
    }
}
