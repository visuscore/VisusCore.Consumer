using System.Collections.Concurrent;

namespace VisusCore.Consumer.Core.Services;

public abstract class QueuedVideoStreamSegmentConsumerContextAccessorBase<TContext>
    where TContext : QueuedVideoStreamSegmentConsumerContextBase
{
    private readonly ConcurrentDictionary<string, TContext> _contexts = new();

    public bool IsExists(string streamId) => _contexts.ContainsKey(streamId);

    public async Task InvokeLockedAsync(
        string streamId,
        Func<TContext, Task> actionAsync,
        bool createIfNotExists = true,
        CancellationToken cancellationToken = default)
    {
        var context = createIfNotExists
            ? GetOrAddContext(streamId)
            : _contexts[streamId];

        await context.ConsumeLock.WaitAsync(cancellationToken);

        try
        {
            await actionAsync(context);
        }
        finally
        {
            context.ConsumeLock.Release();
        }
    }

    public async Task<TResult> InvokeLockedAsync<TResult>(
        string streamId,
        Func<TContext, Task<TResult>> actionAsync,
        bool createIfNotExists = true,
        CancellationToken cancellationToken = default)
    {
        var context = createIfNotExists
            ? GetOrAddContext(streamId)
            : _contexts[streamId];

        await context.ConsumeLock.WaitAsync(cancellationToken);

        try
        {
            return await actionAsync(context);
        }
        finally
        {
            context.ConsumeLock.Release();
        }
    }

    protected abstract TContext CreateContext(string streamId);

    private TContext GetOrAddContext(string streamId) =>
        _contexts.GetOrAdd(streamId, CreateContext);
}
