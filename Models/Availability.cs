namespace HotelManagementSystem.Models
{
    public class Availability
    {
        public required string RoomId { get; set; } = null!;
        private DateTime checkIn;

        //check-in date, only date part is considered
        public DateTime CheckIn
        {
            get => checkIn;
            set => checkIn = value.Date;  
        }

        private DateTime checkOut;
        
        //check-out date, only date part is considered
        public DateTime CheckOut
        {
            get => checkOut;
            set => checkOut = value.Date;  
        }
        public required bool IsAvailable { get; set; }
    }
}
