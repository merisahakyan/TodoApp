using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppTest.Database
{
    public class ListItem
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public TodoList List { get; set; }
        public bool Open { get; set; }
    }
}
