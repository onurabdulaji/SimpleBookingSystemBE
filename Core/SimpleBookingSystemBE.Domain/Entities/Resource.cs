namespace SimpleBookingSystemBE.Domain.Entities
{
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
