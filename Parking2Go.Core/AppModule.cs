using System.Runtime.CompilerServices;
using Eventuous;

namespace Parking2Go.Core;

static class AppModule
{
    [ModuleInitializer]
    //[SuppressMessage("Usage", "CA2255", MessageId = "The \'ModuleInitializer\' attribute should not be used in libraries")]
    internal static void InitializeDomainModule() => TypeMap.RegisterKnownEventTypes();
}