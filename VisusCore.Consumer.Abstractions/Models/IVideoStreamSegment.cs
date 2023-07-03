using System;

namespace VisusCore.Consumer.Abstractions.Models;

/// <summary>
/// Represents a video stream segment.
/// </summary>
public interface IVideoStreamSegment
{
    /// <summary>
    /// Gets the stream id where the segment belongs to.
    /// </summary>
    string StreamId { get; }

    /// <summary>
    /// Gets the fragmented mp4 initialization data.
    /// </summary>
    ReadOnlySpan<byte> Init { get; }

    /// <summary>
    /// Gets the fragmented mp4 data.
    /// </summary>
    ReadOnlySpan<byte> Data { get; }
}
