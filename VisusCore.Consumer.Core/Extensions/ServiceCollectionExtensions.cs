using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using VisusCore.Consumer.Abstractions.Services;

namespace VisusCore.Consumer.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddVideoStreamSegmentConsumer<TVideoStreamSegmentConsumer>(this IServiceCollection services)
        where TVideoStreamSegmentConsumer : class, IVideoStreamSegmentConsumer
    {
        services.AddScoped<TVideoStreamSegmentConsumer>();
        services.TryAddEnumerable(
            ServiceDescriptor.Scoped<IVideoStreamSegmentConsumer, TVideoStreamSegmentConsumer>(services =>
                services.GetRequiredService<TVideoStreamSegmentConsumer>()));

        return services;
    }
}
