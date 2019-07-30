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
    public class TodoListsController : ControllerBase
    {
        private TodoContext _context;
        public TodoListsController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("user/{userId}")]
        public async Task<ResponseModel> GetUserListsAsync(int userId)
        {
            try
            {
                var data = await _context.TodoLists.Where(l => l.UserId == userId)
                                                   .Select(l => new ListViewModel
                                                   {
                                                       Id = l.Id,
                                                       Title = l.Title
                                                   }).ToListAsync();
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
        public async Task<ResponseModel> GetListAsync(int id)
        {
            try
            {
                var data = await _context.TodoLists.Include(l => l.ListItems)
                                         .FirstOrDefaultAsync(u => u.Id == id);
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
        public async Task<ResponseModel> CreateListAsync([FromBody] TodoList list)
        {
            try
            {
                _context.TodoLists.Add(list);
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
        public async Task<ResponseModel> UpdateListAsync(int id, [FromBody] TodoList list)
        {
            try
            {
                var dbList = await _context.TodoLists.FirstOrDefaultAsync(u => u.Id == id);

                if (dbList == null)
                    return new ResponseModel
                    {
                        Status = HttpStatusCode.NotFound,
                    };

                dbList.Title = list.Title;

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
        public async Task<ResponseModel> DeleteListAsync(int id)
        {
            try
            {
                var dbList = await _context.TodoLists.FirstOrDefaultAsync(u => u.Id == id);
                if (dbList == null)
                    return new ResponseModel
                    {
                        Status = HttpStatusCode.NotFound,
                    };
                _context.TodoLists.Remove(dbList);

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