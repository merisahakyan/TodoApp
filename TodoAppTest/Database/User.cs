using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAppTest.Enums;

namespace TodoAppTest.Database
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
        public IEnumerable<TodoList> TodoLists { get; set; }
    }
}
