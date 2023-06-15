using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using RoomBookingApp.Domain;
using RoomBookingApp.Model;
using RoomBookingApp.Processor;
using Shouldly;
using Xunit;

namespace RoomBookingApp.Core.Tests
{
    public class RoomBookingRequestProcessorTest
    {

        private readonly RoomBookingRequestProcessor _processor;

        private readonly RoomBookingRequest _request;

        private readonly Mock<IRoomBookingService> _roomBookingServiceMock;

        private readonly List<Room> _rooms;

        public RoomBookingRequestProcessorTest()
        {

            _rooms = new List<Room>() { new Room() };
            _request = new RoomBookingRequest
            {
                FullName = "Test Name",
                Email = "test@mail.com",
                Date = new DateTimeOffset(2021, 10, 20, 0, 0, 0, TimeSpan.Zero)
            };
            _roomBookingServiceMock = new Mock<IRoomBookingService>();
            _roomBookingServiceMock.Setup(r => r.GetAvailableRooms(_request.Date))
                .Returns(_rooms);

            _processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);

        }

        [Fact]
        public void Should_Return_Room_Booking_Response_With_Request_Values()
        {


            // Act
            BookingResult result = _processor.BookRoom(_request);


            // Assert
            // Assert.NotNull(result);
            // Assert.Equal(request.FullName, result.FullName);
            // Assert.Equal(request.Email, result.Email);
            // Assert.Equal(request.Date, result.Date);


            result.ShouldNotBeNull();
            _request.FullName.ShouldBe(result.FullName);
            _request.Email.ShouldBe(result.Email);
            _request.Date.ShouldBe(result.Date);

        }

        [Fact]
        public void Should_Thrown_Exception_For_Null_Request()
        {
            var exception = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null));
            exception.ParamName.ShouldBe("bookingRequest");
        }

        [Fact]
        public void Should_Save_Room_Booking_Request()
        {
            RoomBooking savedBooking = null;
            _roomBookingServiceMock.Setup(x => x.Save(It.IsAny<RoomBooking>()))
                .Callback<RoomBooking>(booking =>
                {
                    savedBooking = booking;
                });

            _processor.BookRoom(_request);

            _roomBookingServiceMock.Verify(x => x.Save(It.IsAny<RoomBooking>()), Times.Once);

            savedBooking.ShouldNotBeNull();
            savedBooking.FullName.ShouldBe(_request.FullName);
            savedBooking.Email.ShouldBe(_request.Email);
            savedBooking.Date.ShouldBe(_request.Date);

        }

        [Fact]
        public void Should_Not_Save_Room_Booking_Request_If_Not_Available()
        {
            _rooms.Clear();
            _processor.BookRoom(_request);
            _roomBookingServiceMock.Verify(x => x.Save(It.IsAny<RoomBooking>()), Times.Never);

        }
    }
}