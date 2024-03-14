using CommunityToolkit.HighPerformance;

namespace TkMerger.Core.IO;

public static class BlockResourceMetadata
{
    private static readonly string _blockInfoPath = Path.Combine("Resources", "~meta");

    public static readonly Dictionary<ulong, BlockResourceMetadataEntry> Entries = [];

    static BlockResourceMetadata()
    {
        using FileStream fs = File.OpenRead(_blockInfoPath);
        while (fs.Position < fs.Length) {
            ulong hash = fs.Read<ulong>();
            BlockResourceMetadataEntry entry = new() {
                BlockId = fs.Read<ulong>(),
                Offset = fs.Read<long>(),
                Size = fs.Read<int>()
            };

            Entries.Add(hash, entry);
        }
    }
}
