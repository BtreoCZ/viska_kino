using System.ComponentModel.DataAnnotations;

namespace viska_kino_mvc.Models
{
    public class CreateReservationViewModel
    {
        [Required(ErrorMessage = "ID promítání je povinné.")]
        public int ScreeningId { get; set; }

        [Required(ErrorMessage = "ID filmu je povinné.")]
        public int FilmId { get; set; }

        [Required(ErrorMessage = "Název filmu je povinný.")]
        public string FilmTitle { get; set; }

        [Required(ErrorMessage = "Čas promítání je povinný.")]
        public DateTime ScreeningTime { get; set; }

        [Required(ErrorMessage = "ID sálu je povinné.")]
        public int ScreeningRoomId { get; set; }

        [Required(ErrorMessage = "Název sálu je povinný.")]
        public string ScreeningRoomName { get; set; }

        [Required(ErrorMessage = "Počet míst je povinný.")]
        [Range(1, 10, ErrorMessage = "Počet míst musí být mezi 1 a 10.")]
        public int NumberOfSeats { get; set; }
    }
}
