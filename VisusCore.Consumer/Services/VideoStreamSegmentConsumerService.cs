using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VisusCore.AidStack.Extensions;
using VisusCore.Consumer.Abstractions.Models;
using VisusCore.Consumer.Abstractions.Services;

namespace VisusCore.Consumer.Services;

public class VideoStreamSegmentConsumerService : IVideoStreamSegmentConsumerService
{
    private readonly IEnumerable<IVideoStreamSegmentConsumer> _videoStreamSegmentConsumers;
    private readonly ILogger _logger;

    public VideoStreamSegmentConsumerService(
        IEnumerable<IVideoStreamSegmentConsumer> videoStreamSegmentConsumers,
        ILogger<VideoStreamSegmentConsumerService> logger)
    {
        _videoStreamSegmentConsumers = videoStreamSegmentConsumers;
        _logger = logger;
    }

    public Task ConsumeAsync(IVideoStreamSegment segment, CancellationToken cancellationToken = default) =>
        _videoStreamSegmentConsumers.AwaitEachAsync(
            async consumer =>
            {
                try
                {
                    await consumer.ConsumeAsync(segment, cancellationToken);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Error consuming video stream segment.");
                }
            },
            cancellationToken);
}
