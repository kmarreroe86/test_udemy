namespace RoomBookingApp.Domain;

public class RoomBooking : RoomBookingBase
{
    public int RoomId { get; set; }
    public int? Id { get; set; }
    public Room Room { get; set; }
}
