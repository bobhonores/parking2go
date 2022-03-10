using System.Text.Json;
using Eventuous;
using Eventuous.Diagnostics.OpenTelemetry;
using Eventuous.EventStore;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Parking2Go.Core.ParkingSlots.Application;
using Parking2Go.Core.ParkingSlots.Domain;

namespace Parking2Go.WebApi.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddEventuous(this IServiceCollection services)
    {
        DefaultEventSerializer.SetDefaultSerializer(
            new DefaultEventSerializer(
                new JsonSerializerOptions(JsonSerializerDefaults.Web).ConfigureForNodaTime(DateTimeZoneProviders.Tzdb)
            )
        );

        services.AddEventStoreClient("esdb://localhost:2113?tls=false");
        services.AddAggregateStore<EsdbEventStore>();
        services.AddApplicationService<ParkingSlotServices.CommandService, ParkingSlot>();
    }

    public static void AddOpenTelemetry(this IServiceCollection services)
    {
        services.AddOpenTelemetryTracing(
            builder => builder
                .AddAspNetCoreInstrumentation()
                .AddGrpcClientInstrumentation()
                .AddEventuousTracing()
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("parkingslots"))
                .SetSampler(new AlwaysOnSampler())
                .AddZipkinExporter()
        );
    }
}
