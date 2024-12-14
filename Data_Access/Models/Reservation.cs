using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public User User { get; set; } 
        public int ScreeningId { get; set; } 
        public Screening Screening { get; set; } 
        public int NumberOfSeats { get; set; } 
    }
}
