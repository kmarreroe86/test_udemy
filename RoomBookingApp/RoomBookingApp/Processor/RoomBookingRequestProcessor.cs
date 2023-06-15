using RoomBookingApp.Domain;
using RoomBookingApp.Model;

namespace RoomBookingApp.Processor
{
    public class RoomBookingRequestProcessor
    {

        public IRoomBookingService _roomBookingService;


        public RoomBookingRequestProcessor(IRoomBookingService roomBookingService)
        {
            _roomBookingService = roomBookingService;
        }

        public BookingResult BookRoom(RoomBookingRequest bookingRequest)
        {
            if (bookingRequest is null) throw new ArgumentNullException(nameof(bookingRequest));

            var availableRooms = _roomBookingService.GetAvailableRooms(bookingRequest.Date);
            if (availableRooms.Any())
            {
                _roomBookingService.Save(CreateRoomBookingObject<RoomBooking>(bookingRequest));
            }

            return CreateRoomBookingObject<BookingResult>(bookingRequest);
        }

        private static TRoomBooking CreateRoomBookingObject<TRoomBooking>(RoomBookingRequest bookingRequest)
            where TRoomBooking : RoomBookingBase, new()
        {
            return new TRoomBooking
            {
                FullName = bookingRequest.FullName,
                Email = bookingRequest.Email,
                Date = bookingRequest.Date,
            };
        }
    }
}