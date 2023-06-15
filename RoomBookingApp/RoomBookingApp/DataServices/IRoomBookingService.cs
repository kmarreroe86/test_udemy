using RoomBookingApp.Domain;

namespace RoomBookingApp;
public interface IRoomBookingService
{
    void Save(RoomBooking roomBooking);

    IEnumerable<Room> GetAvailableRooms(DateTimeOffset dateTimeOffset);
}
