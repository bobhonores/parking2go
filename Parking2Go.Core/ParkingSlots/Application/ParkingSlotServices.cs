using Eventuous;
using Parking2Go.Core.ParkingSlots.Domain;
using static Parking2Go.Core.ParkingSlots.Domain.ValueObjects;

namespace Parking2Go.Core.ParkingSlots.Application;
public static class ParkingSlotServices
{
    public static class Commands
    {
        public record Register(
            string ParkingSlotId,
            DateTimeOffset CreatedAt,
            double Latitude,
            double Longitude,
            string Building,
            string Floor
        );

        public record Occupy(
            string ParkingSlotId,
            string LicensePlate,
            DateTimeOffset OccupiedAt
        );

        public record Clear(
            string ParkingSlotId,
            string LicensePlate,
            DateTimeOffset ClearedAt
        );
    }


    public class CommandService : ApplicationService<ParkingSlot, ParkingSlotState, ParkingSlotId>
    {
        public CommandService(IAggregateStore store) : base(store)
        {
            OnNewAsync<Commands.Register>(
                (parkingSlot, cmd, _) =>
                {
                    parkingSlot.Register(
                        new ParkingSlotId(cmd.ParkingSlotId),
                        new Happening { Value = cmd.CreatedAt },
                        new GeoLocation(cmd.Latitude, cmd.Longitude),
                        new ReferenceLocation(cmd.Building, cmd.Floor)
                    );
                    return Task.CompletedTask;
                }
            );

            OnExistingAsync<Commands.Occupy>(
                cmd => new ParkingSlotId(cmd.ParkingSlotId),
                (parkingSlot, cmd, _) =>
                {
                    parkingSlot.Occupy(
                        new LicensePlate(cmd.LicensePlate),
                        new Happening { Value = cmd.OccupiedAt }
                    );
                    return Task.CompletedTask;
                }
            );

            OnExistingAsync<Commands.Clear>(
                cmd => new ParkingSlotId(cmd.ParkingSlotId),
                (parkingSlot, cmd, _) =>
                {
                    parkingSlot.Clear(
                        new LicensePlate(cmd.LicensePlate),
                        new Happening { Value = cmd.ClearedAt }
                    );
                    return Task.CompletedTask;
                }
            );
        }
    }
}