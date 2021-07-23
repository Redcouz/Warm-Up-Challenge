using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warm_Up_Challenge.Models
{
    public class Form
    {
        public int FormId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
        public DateTime CreationDate { get; set; }

        public Form()
        {

        }
    }
}
