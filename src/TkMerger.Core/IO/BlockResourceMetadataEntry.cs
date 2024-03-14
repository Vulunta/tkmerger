namespace TkMerger.Core.IO;

public class BlockResourceMetadataEntry
{
    public required ulong BlockId { get; init; }
    public required long Offset { get; init; }
    public required int Size { get; init; }
}
