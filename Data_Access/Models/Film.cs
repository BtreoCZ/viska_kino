using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Models
{
    public class Film
    {
        public int Id { get; set; } 
        public string Title { get; set; } 
        public string Director { get; set; } 
        public int DurationMinutes { get; set; } 
        public string Genre { get; set; } 
    }
}
