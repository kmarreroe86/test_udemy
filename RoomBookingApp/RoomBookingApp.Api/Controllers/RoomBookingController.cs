using Microsoft.AspNetCore.Mvc;
using RoomBookingApp.Enums;
using RoomBookingApp.Model;

namespace RoomBookingApp.Api;

[ApiController]
[Route("[controller]")]
public class RoomBookingController : Controller
{
    private IRoomBookingRequestProcessor processor;

    public RoomBookingController(IRoomBookingRequestProcessor processor)
    {
        this.processor = processor;
    }

    [HttpPost]
    public async Task<IActionResult> BookRoom(RoomBookingRequest request)
    {
        if (ModelState.IsValid)
        {
            var result = processor.BookRoom(request);
            if (result.Flag == BookingSuccessFlag.Success)
            {
                return Ok(result);
            }

            ModelState.AddModelError(nameof(RoomBookingRequest.Date), "No rooms available for the given date");
        }

        return BadRequest(ModelState);



    }
}
