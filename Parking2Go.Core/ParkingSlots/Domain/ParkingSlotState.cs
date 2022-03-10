using Eventuous;
using static Parking2Go.Core.ParkingSlots.Domain.ValueObjects;
using static Parking2Go.Core.ParkingSlots.Domain.Events;

namespace Parking2Go.Core.ParkingSlots.Domain;

public record ParkingSlotState : AggregateState<ParkingSlotState, ParkingSlotId>
{
    public GeoLocation Location { get; init; }
    public ReferenceLocation Reference { get; init; }
    public Status Status { get; init; }
    public Timeline Timeline { get; init; }

    public ParkingSlotState()
    {
        On<V1.ParkingSlotRegistered>(HandleRegister);
        On<V1.ParkingSlotOccupied>(HandleOccupy);
        On<V1.ParkingSlotCleared>(HandleClear);
    }

    static ParkingSlotState HandleRegister(ParkingSlotState state, V1.ParkingSlotRegistered @event)
        => state with
        {
            Id = new ParkingSlotId(@event.ParkingSlotId),
            Reference = new ReferenceLocation(@event.Building, @event.Floor),
            Location = new GeoLocation(@event.Latitude, @event.Longitude),
            Status = new Status { Value = ValueObjects.ParkingSlotStatus.Free },
            Timeline = new Timeline { Created = @event.CreatedAt }
        };
    static ParkingSlotState HandleOccupy(ParkingSlotState state, V1.ParkingSlotOccupied @event)
        => state with
        {
            Id = new ParkingSlotId(@event.ParkingSlotId),
            Status = new Status { Value = ValueObjects.ParkingSlotStatus.Occupied },
            Timeline = new Timeline { Created = state.Timeline.Created, OccupiedAt = @event.OccupiedAt }
        };

    static ParkingSlotState HandleClear(ParkingSlotState state, V1.ParkingSlotCleared @event)
        => state with
        {
            Id = new ParkingSlotId(@event.ParkingSlotId),
            Status = new Status { Value = ValueObjects.ParkingSlotStatus.Free },
            Timeline = new Timeline { Created = state.Timeline.Created }
        };
}
