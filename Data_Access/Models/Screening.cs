using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Models
{
    public class Screening
    {
        public int Id { get; set; }
        public DateTime ScreeningTime { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public int ScreeningRoomId { get; set; }
        public ScreeningRoom ScreeningRoom { get; set; }

        public int Capacity { get; set; }
    }

}
