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
    public class ListItemsController : ControllerBase
    {
        private TodoContext _context;
        public ListItemsController(TodoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ResponseModel> AddListItemAsync([FromBody] ListItemModel listItem)
        {
            try
            {
                _context.ListItems.Add(new ListItem
                {
                    ListId = listItem.ListId,
                    Name = listItem.Name,
                    Open = listItem.Open
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
        public async Task<ResponseModel> UpdateListItemAsync([FromBody] ListItemModel listItem)
        {
            try
            {
                var dbList = await _context.ListItems.FirstOrDefaultAsync(u => u.Id == listItem.Id);

                if (dbList == null)
                    return new ResponseModel
                    {
                        Status = HttpStatusCode.NotFound,
                    };

                dbList.Name = listItem.Name;
                dbList.Open = listItem.Open;

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
                var dbList = await _context.ListItems.FirstOrDefaultAsync(u => u.Id == id);
                if (dbList == null)
                    return new ResponseModel
                    {
                        Status = HttpStatusCode.NotFound,
                    };
                _context.ListItems.Remove(dbList);

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