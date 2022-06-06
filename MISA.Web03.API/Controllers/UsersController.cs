using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Web03.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Xử lí sự kiện login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //POST api/<UsersController>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            try
            {
                var user = AuthenticateUser(login.Email, login.Password);
                if (user == null)
                {
                    return BadRequest();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("Username", user.Username),

                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity)
                    );

                return Ok(user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout")]
        [AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {
            //SignOutAsync is Extension method for SignOut    
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page    
            return Ok();
        }

        /// <summary>
        /// Kiểm tra email và password có hợp lệ khong
        /// </summary>
        /// <param name="Email"> Email người dùng nhập </param>
        /// <param name="Password"> Password ngườu dùng nhập </param>
        /// <returns> Nếu hợp lệ - Thông tin user đã đang nhập, Nếu không hợp lệ - trả về null  </returns>
        private User AuthenticateUser(string email, string password)
        {
            // TODO: mã hóa password, làm thêm task (bất đồng độ)
            // băm hash256, check key 
            if (email == "user@mail" && password == "12345")
            {
                return new User()
                {
                    Email = "user@mail",
                    FirstName ="Lê",
                    LastName ="Tuấn",
                    Username = "Tit"
                };
            }
            else
            {
                return null;
            }
        }
    }
}
