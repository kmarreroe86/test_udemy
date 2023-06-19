using RoomBookingApp.Model;

namespace RoomBookingApp;
public interface IRoomBookingRequestProcessor
{
    BookingResult BookRoom(RoomBookingRequest bookingRequest);
}
