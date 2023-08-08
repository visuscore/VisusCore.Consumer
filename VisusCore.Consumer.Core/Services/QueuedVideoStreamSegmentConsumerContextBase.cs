using System.Collections.Concurrent;
using VisusCore.Consumer.Abstractions.Models;

namespace VisusCore.Consumer.Core.Services;

public abstract class QueuedVideoStreamSegmentConsumerContextBase
{
    public string StreamId { get; }
    public SemaphoreSlim ConsumeLock { get; } = new(1, 1);
    public ConcurrentFixedSizeQueue<IVideoStreamSegment> Queue { get; }

    protected QueuedVideoStreamSegmentConsumerContextBase(string streamId, int queueSize)
    {
        StreamId = streamId;
        Queue = new ConcurrentFixedSizeQueue<IVideoStreamSegment>(queueSize);
    }
}
