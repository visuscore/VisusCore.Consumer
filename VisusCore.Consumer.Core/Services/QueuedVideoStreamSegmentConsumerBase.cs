using VisusCore.Consumer.Abstractions.Models;
using VisusCore.Consumer.Abstractions.Services;

namespace VisusCore.Consumer.Core.Services;

public abstract class QueuedVideoStreamSegmentConsumerBase<TContext> : IVideoStreamSegmentConsumer
    where TContext : QueuedVideoStreamSegmentConsumerContextBase
{
    private readonly QueuedVideoStreamSegmentConsumerContextAccessorBase<TContext> _contextAccessor;

    protected QueuedVideoStreamSegmentConsumerBase(QueuedVideoStreamSegmentConsumerContextAccessorBase<TContext> contextAccessor) =>
        _contextAccessor = contextAccessor;

    public Task ConsumeAsync(IVideoStreamSegment segment, CancellationToken cancellationToken = default)
    {
        if (segment is null)
        {
            throw new ArgumentNullException(nameof(segment));
        }

        return _contextAccessor.InvokeLockedAsync(
            segment.StreamId,
            context =>
            {
                context.Queue.Enqueue(segment);

                return SegmentQueuedAsync(context, segment, cancellationToken);
            },
            cancellationToken: cancellationToken);
    }

    protected virtual Task SegmentQueuedAsync(
        TContext context,
        IVideoStreamSegment segment,
        CancellationToken cancellationToken = default) =>
        Task.CompletedTask;
}
