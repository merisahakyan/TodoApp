using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAppTest.Database;
using TodoAppTest.Models;

namespace TodoAppTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private TodoContext _context;
        public UsersController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ResponseModel> GetUsersAsync()
        {
            try
            {
                var data = await _context.Users.ToListAsync();
                return new ResponseModel
                {
                    Data = data,
                    Status = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new ResponseModel
                {
                    Message = e.Message,
                    Status = HttpStatusCode.InternalServerError,
                };
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ResponseModel> GetUserAsync(int id)
        {
            try
            {
                var data = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                return new ResponseModel
                {
                    Data = data,
                    Status = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new ResponseModel
                {
                    Message = e.Message,
                    Status = HttpStatusCode.InternalServerError,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> CreateUserAsync([FromBody] UserModel user)
        {
            try
            {
                _context.Users.Add(new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Role = Enums.Roles.User,
                });
                await _context.SaveChangesAsync();

                return new ResponseModel
                {
                    Status = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new ResponseModel
                {
                    Message = e.Message,
                    Status = HttpStatusCode.InternalServerError,
                };
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ResponseModel> UpdateUserAsync(int id, [FromBody] UserModel user)
        {
            try
            {
                var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (dbUser == null)
                    return new ResponseModel
                    {
                        Status = HttpStatusCode.NotFound,
                    };

                dbUser.Name = user.Name;
                dbUser.Email = user.Email;
                dbUser.Password = user.Password;

                _context.Entry(dbUser).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return new ResponseModel
                {
                    Status = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new ResponseModel
                {
                    Message = e.Message,
                    Status = HttpStatusCode.InternalServerError,
                };
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ResponseModel> DeleteUserAsync(int id)
        {
            try
            {
                var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (dbUser == null)
                    return new ResponseModel
                    {
                        Status = HttpStatusCode.NotFound,
                    };
                _context.Users.Remove(dbUser);

                await _context.SaveChangesAsync();

                return new ResponseModel
                {
                    Status = HttpStatusCode.OK
                };
            }
            catch (Exception e)
            {
                return new ResponseModel
                {
                    Message = e.Message,
                    Status = HttpStatusCode.InternalServerError,
                };
            }
        }
    }
}