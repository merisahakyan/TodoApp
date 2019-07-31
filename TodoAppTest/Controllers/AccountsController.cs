using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAppTest.Database;
using TodoAppTest.Models;

namespace TodoAppTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private TodoContext _context;
        public AccountsController(TodoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ResponseModel> LoginAsync([FromBody] LoginModel login)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email && u.Password == login.Password);
                if (user == null)
                    return new ResponseModel
                    {
                        Status = System.Net.HttpStatusCode.NotFound,
                    };

                var claims = new List<Claim>
                        {
                          new Claim(ClaimTypes.Name, user.Id.ToString())
                        };

                var claimsIdentity = new ClaimsIdentity(
                  claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(
                  CookieAuthenticationDefaults.AuthenticationScheme,
                  new ClaimsPrincipal(claimsIdentity),
                  authProperties);

                return new ResponseModel
                {
                    Status = System.Net.HttpStatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new ResponseModel
                {
                    Status = System.Net.HttpStatusCode.InternalServerError,
                    Message = e.Message,
                };
            }
        }

        [HttpGet]
        public async Task LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}