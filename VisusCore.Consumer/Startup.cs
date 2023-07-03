using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using VisusCore.Consumer.Abstractions.Services;
using VisusCore.Consumer.Services;

namespace VisusCore.Consumer;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services) =>
        services.AddScoped<IVideoStreamSegmentConsumerService, VideoStreamSegmentConsumerService>();
}
