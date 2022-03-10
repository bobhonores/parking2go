using Eventuous;
using static Parking2Go.Core.ParkingSlots.Domain.Events;
using static Parking2Go.Core.ParkingSlots.Domain.ValueObjects;

namespace Parking2Go.Core.ParkingSlots.Domain;
public class ParkingSlot : Aggregate<ParkingSlotState, ParkingSlotId>
{
    public void Register(ParkingSlotId parkingSlotId,
                        Happening createdAt,
                        GeoLocation location,
                        ReferenceLocation reference)
    {
        EnsureDoesntExist();

        Apply(new V1.ParkingSlotRegistered(
            parkingSlotId.Value,
            createdAt.Value,
            location.Latitude,
            location.Longitude,
            reference.Building,
            reference.Floor
        ));
    }
    public void Occupy(LicensePlate licensePlate, Happening occupiedAt)
    {
        EnsureExists();

        if (State.Status.Value == ValueObjects.ParkingSlotStatus.Occupied)
        {
            throw new DomainException("Parking Slot is not available");
        }

        Apply(new V1.ParkingSlotOccupied(
            GetId(),
            licensePlate.Value,
            occupiedAt.Value
        ));
    }

    public void Clear(LicensePlate licensePlate, Happening clearedAt)
    {
        EnsureExists();

        if (State.Status.Value == ValueObjects.ParkingSlotStatus.Free)
        {
            throw new DomainException("Parking Slot is already clear");
        }

        Apply(new V1.ParkingSlotCleared(
            GetId(),
            licensePlate.Value,
            clearedAt.Value
        ));
    }
}