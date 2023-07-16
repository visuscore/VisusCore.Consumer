namespace VisusCore.Consumer.Abstractions.Models;

/// <summary>
/// Describes a video stream segment.
/// </summary>
public interface IVideoStreamSegmentMetadata
{
    /// <summary>
    /// Gets the stream id where the initialization data belongs to.
    /// </summary>
    string StreamId { get; }

    /// <summary>
    /// Gets the timestamp elapsed in microseconds since Unix epoch.
    /// </summary>
    long TimestampUtc { get; }

    /// <summary>
    /// Gets the segment duration in microseconds.
    /// </summary>
    long Duration { get; }

    /// <summary>
    /// Gets the timestamp provided by the device in microseconds since Unix epoch.
    /// </summary>
    long? TimestampProvided { get; }

    /// <summary>
    /// Gets the number of frames in the segment.
    /// </summary>
    long FrameCount { get; }
}
