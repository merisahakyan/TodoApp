using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoAppTest.Database;

namespace TodoAppTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private User _user = null;
        private int _userId;
        private TodoContext _context;
        public BaseController(TodoContext context)
        {

            _context = context;
        }

        public User User
        {
            get
            {
                var principal = HttpContext.User as ClaimsPrincipal;
                _userId = int.Parse(principal.Claims.First(c => c.Type == ClaimTypes.Name).Value);
                if (principal != null)
                    _user = _context.Users.FirstOrDefault(u => u.Id == _userId);
                return _user;
            }
        }

        public int UserId
        {
            get
            {
                if (_user == null)
                    return User.Id;
                else
                    return _userId;
            }
        }
    }
}
