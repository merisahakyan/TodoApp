using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppTest.Database
{
    public class TodoList
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public IEnumerable<ListItem> ListItems { get; set; }
    }
}
