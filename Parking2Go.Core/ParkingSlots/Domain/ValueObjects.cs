namespace Parking2Go.Core.ParkingSlots.Domain;

public static class ValueObjects
{
    public record GeoLocation
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public GeoLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }

    public record ReferenceLocation
    {
        public string Building { get; }
        public string Floor { get; }

        public ReferenceLocation(string building, string floor)
        {
            if (string.IsNullOrEmpty(building))
            {
                throw new ArgumentNullException("building");
            }

            if (string.IsNullOrEmpty(floor))
            {
                throw new ArgumentNullException("floor");
            }

            Building = building;
            Floor = floor;
        }
    }

    public record Status
    {
        public ParkingSlotStatus Value { get; init; }
    }

    public enum ParkingSlotStatus
    {
        Free,
        Occupied
    }

    public record Timeline
    {
        public DateTimeOffset Created { get; init; }
        public DateTimeOffset? OccupiedAt { get; init; }
    }

    public record Happening
    {
        public DateTimeOffset Value { get; init; }
    }

    public record LicensePlate
    {
        public string Value { get; }

        public LicensePlate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value");
            }

            Value = value;
        }
    }
}