using Moq;
using Shouldly;
using RoomBookingApp.Enums;
using RoomBookingApp.Model;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RoomBookingApp.DataServices;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using System.Threading.Tasks;


namespace RoomBookingApp.Api.Tests;
public class RoomBookingControllerTests
{
    private Mock<IRoomBookingRequestProcessor> _roomBookingProcessor;
    private RoomBookingController _controller;
    private RoomBookingRequest _request;
    private BookingResult _result;

    public RoomBookingControllerTests()
    {
        _roomBookingProcessor = new Mock<IRoomBookingRequestProcessor>();
        _controller = new RoomBookingController(_roomBookingProcessor.Object);
        _request = new RoomBookingRequest();
        _result = new BookingResult();

        _roomBookingProcessor.Setup(x => x.BookRoom(_request)).Returns(_result);
    }

    [Theory]
    [InlineData(1, true, typeof(OkObjectResult), BookingSuccessFlag.Success)]
    [InlineData(0, false, typeof(BadRequestObjectResult), BookingSuccessFlag.Failure)]
    public async Task Should_Call_Booking_Method_When_Valid(int expectedMethodCalls, bool isModelValid,
        Type expectedActionResultType, BookingSuccessFlag bookingResultFlag)
    {
        // Arrange
        if (!isModelValid)
        {
            _controller.ModelState.AddModelError("Key", "ErrorMessage");
        }

        _result.Flag = bookingResultFlag;


        // Act
        var result = await _controller.BookRoom(_request);

        // Assert
        result.ShouldBeOfType(expectedActionResultType);
        _roomBookingProcessor.Verify(x => x.BookRoom(_request), Times.Exactly(expectedMethodCalls));

    }
}
