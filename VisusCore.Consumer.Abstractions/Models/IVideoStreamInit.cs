using System;

namespace VisusCore.Consumer.Abstractions.Models;

/// <summary>
/// Represents a video stream initialization data.
/// </summary>
public interface IVideoStreamInit
{
    /// <summary>
    /// Gets the stream id where the initialization data belongs to.
    /// </summary>
    string StreamId { get; }

    /// <summary>
    /// Gets the fragmented mp4 initialization data.
    /// </summary>
    ReadOnlySpan<byte> Init { get; }
}
