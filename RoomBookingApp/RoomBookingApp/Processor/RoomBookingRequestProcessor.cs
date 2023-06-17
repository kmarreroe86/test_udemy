using RoomBookingApp.Enums;
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
            var result = CreateRoomBookingObject<BookingResult>(bookingRequest);
            if (availableRooms.Any())
            {
                var room = availableRooms.First();
                var roomBooking = CreateRoomBookingObject<Domain.RoomBooking>(bookingRequest);
                roomBooking.RoomId = room.Id;
                _roomBookingService.Save(roomBooking);

                result.RoomBookingId = roomBooking.Id;
                result.Flag = BookingSuccessFlag.Success;
            }
            else
            {
                result.Flag = BookingSuccessFlag.Failure;
            }

            return result;
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