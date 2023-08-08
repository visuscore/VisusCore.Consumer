using System.Diagnostics.CodeAnalysis;

namespace System.Collections.Concurrent;

public sealed class ConcurrentFixedSizeQueue<T> : IProducerConsumerCollection<T>, IReadOnlyCollection<T>
{
    private readonly ConcurrentQueue<T> concurrentQueue;
    private readonly int maxSize;

    public int Count => concurrentQueue.Count;
    public bool IsEmpty => concurrentQueue.IsEmpty;

    public ConcurrentFixedSizeQueue(int maxSize)
        : this(Array.Empty<T>(), maxSize)
    {
    }

    public ConcurrentFixedSizeQueue(IEnumerable<T> initialCollection, int maxSize)
    {
        if (initialCollection == null)
        {
            throw new ArgumentNullException(nameof(initialCollection));
        }

        concurrentQueue = new ConcurrentQueue<T>(initialCollection);
        this.maxSize = maxSize;
    }

    public void Enqueue(T item)
    {
        concurrentQueue.Enqueue(item);

        if (concurrentQueue.Count > maxSize)
        {
            concurrentQueue.TryDequeue(out _);
        }
    }

    public bool TryPeek([MaybeNullWhen(false)] out T result) => concurrentQueue.TryPeek(out result);
    public bool TryDequeue([MaybeNullWhen(false)] out T result) => concurrentQueue.TryDequeue(out result);

    public void CopyTo(T[] array, int index) => concurrentQueue.CopyTo(array, index);
    public T[] ToArray() => concurrentQueue.ToArray();

    public IEnumerator<T> GetEnumerator() => concurrentQueue.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    void ICollection.CopyTo(Array array, int index) => ((ICollection)concurrentQueue).CopyTo(array, index);
    object ICollection.SyncRoot => ((ICollection)concurrentQueue).SyncRoot;
    bool ICollection.IsSynchronized => ((ICollection)concurrentQueue).IsSynchronized;

    bool IProducerConsumerCollection<T>.TryAdd(T item) =>
        ((IProducerConsumerCollection<T>)concurrentQueue).TryAdd(item);
    bool IProducerConsumerCollection<T>.TryTake([MaybeNullWhen(false)] out T item) =>
        ((IProducerConsumerCollection<T>)concurrentQueue).TryTake(out item);

    public override int GetHashCode() => concurrentQueue.GetHashCode();
    public override bool Equals(object? obj) => concurrentQueue.Equals(obj);
    public override string? ToString() => concurrentQueue.ToString();
}
