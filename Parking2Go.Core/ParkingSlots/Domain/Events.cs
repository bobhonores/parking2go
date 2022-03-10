using Eventuous;

namespace Parking2Go.Core.ParkingSlots.Domain;

public static class Events
{
    public static class V1
    {
        [EventType("V1.ParkingSlotRegistered")]
        public record ParkingSlotRegistered(
            string ParkingSlotId,
            DateTimeOffset CreatedAt,
            double Latitude,
            double Longitude,
            string Building,
            string Floor
        );

        [EventType("V1.ParkingSlotOccupied")]
        public record ParkingSlotOccupied(
            string ParkingSlotId,
            string LicensePlate,
            DateTimeOffset OccupiedAt
        );

        [EventType("V1.ParkingSlotCleared")]
        public record ParkingSlotCleared(
            string ParkingSlotId,
            string LicensePlate,
            DateTimeOffset ClearedAt
        );
    }
}
