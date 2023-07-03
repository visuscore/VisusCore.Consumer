using System.Threading;
using System.Threading.Tasks;
using VisusCore.Consumer.Abstractions.Models;

namespace VisusCore.Consumer.Abstractions.Services;

/// <summary>
/// Represents a root consumer of video stream segments.
/// </summary>
public interface IVideoStreamSegmentConsumerService
{
    /// <summary>
    /// Consumes a video stream segment.
    /// </summary>
    Task ConsumeAsync(IVideoStreamSegment segment, CancellationToken cancellationToken = default);
}
