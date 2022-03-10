using Eventuous;
using Eventuous.AspNetCore.Web;
using Microsoft.AspNetCore.Mvc;
using Parking2Go.Core.ParkingSlots.Domain;
using static Parking2Go.Core.ParkingSlots.Application.ParkingSlotServices.Commands;

namespace Parking2Go.WebApi.Controllers;

[Route("/api/v1/parkingslots")]
public class ParkingSlotController : CommandHttpApiBase<ParkingSlot>
{
    readonly IAggregateStore _store;

    public ParkingSlotController(IApplicationService<ParkingSlot> service, IAggregateStore store) : base(service)
    {
        _store = store;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ParkingSlotState> Get(string id, CancellationToken cancellationToken)
    {
        var booking = await _store.Load<ParkingSlot, ParkingSlotState, ParkingSlotId>(new ParkingSlotId(id), cancellationToken);
        return booking.State;
    }

    [HttpPost]
    [Route("")]
    public Task<ActionResult<Result>> Register([FromBody] Register cmd, CancellationToken cancellationToken)
        => Handle(cmd, cancellationToken);

    [HttpPost]
    [Route("occupy")]
    public Task<ActionResult<Result>> Occupy([FromBody] Occupy cmd, CancellationToken cancellationToken)
        => Handle(cmd, cancellationToken);

    [HttpPost]
    [Route("clear")]
    public Task<ActionResult<Result>> Clear([FromBody] Clear cmd, CancellationToken cancellationToken)
        => Handle(cmd, cancellationToken);
}
