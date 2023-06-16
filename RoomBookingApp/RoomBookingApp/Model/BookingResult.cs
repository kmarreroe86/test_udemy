using RoomBookingApp.Enums;

namespace RoomBookingApp.Model
{
    public class BookingResult : RoomBookingBase
    {

        public BookingSuccessFlag Flag { get; set; }

        public int? RoomBookingId { get; set; }
    }
}