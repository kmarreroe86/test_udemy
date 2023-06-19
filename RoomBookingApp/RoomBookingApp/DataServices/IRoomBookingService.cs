using RoomBookingApp.Domain;

namespace RoomBookingApp.DataServices;
public interface IRoomBookingService
{
    void Save(RoomBooking roomBooking);

    IEnumerable<Room> GetAvailableRooms(DateTimeOffset dateTimeOffset);
}
