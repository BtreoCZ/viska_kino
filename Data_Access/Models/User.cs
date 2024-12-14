namespace Data_Access.Models
{
    public class User
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public string Email { get; set; } 
        public string PhoneNumber { get; set; } 
        public string Login { get; set; }
        public string Password { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
    }

}