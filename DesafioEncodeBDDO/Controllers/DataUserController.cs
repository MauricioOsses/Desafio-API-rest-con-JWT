using DesafioEncodeBDDO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DesafioBackEnd.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DateUserController : ControllerBase
    {
        public readonly ApplicationContext _dbContext;
        public DateUserController(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("Users")]
        public IActionResult Get()
        {
            List<DataUser> dataUsers = new List<DataUser>();

            try
            {
                dataUsers = _dbContext.DataUser.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = dataUsers });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = dataUsers });

            }
        }
        [HttpGet]
        [Route("Users/{IdUser:int}")]
        public IActionResult GetUser(int IdUser)
        {
            DataUser user = _dbContext.DataUser.Find(IdUser);

            if (user == null)
            {
                return BadRequest("Usuario no encontrado...");
            }

            try
            {

                user = _dbContext.DataUser.Where(us => us.IdDataUser == IdUser).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", Response = user });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, Response = user });

            }
        }
        
        [Authorize(Policy = "Admin")]
        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] DataUser usr)
        {
            DataUser User = _dbContext.DataUser.Find(usr.IdDataUser);

            try
            {
                User.Name = usr.Name is null ? User.Name : usr.Name;
                User.Surname = usr.Surname is null ? User.Surname : usr.Surname;
                User.Dni = usr.Dni is null ? User.Dni : usr.Dni;

                _dbContext.DataUser.Update(User);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Los datos fueron actualizados" });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }
        }
        
        [Authorize(Policy = "Admin")]
        [HttpDelete]
        [Route("Delete/{IdUser:int}")]
        public IActionResult Delete(int IdUser)
        {
            DataUser User = _dbContext.DataUser.Find(IdUser);

            if (User == null)
            {
                return BadRequest("Usuario no encontrado...");
            }

            try
            {

                _dbContext.DataUser.Remove(User);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Los datos fueron eliminados" });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });

            }
        }
    }
}
