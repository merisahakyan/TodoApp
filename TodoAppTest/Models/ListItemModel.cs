using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppTest.Models
{
    public class ListItemModel
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public string Name { get; set; }
        public bool Open { get; set; }
    }
}
