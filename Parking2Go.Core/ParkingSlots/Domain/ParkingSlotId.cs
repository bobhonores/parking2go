using Eventuous;

namespace Parking2Go.Core.ParkingSlots.Domain;
public record ParkingSlotId(string Value) : AggregateId(Value);