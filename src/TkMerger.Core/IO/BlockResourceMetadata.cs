using CommunityToolkit.HighPerformance;

namespace TkMerger.Core.IO;

public static class BlockResourceMetadata
{
    private static bool _isLoaded = false;

    public static readonly Dictionary<ulong, BlockResourceMetadataEntry> Entries = [];

    public static async Task Load()
    {
        if (_isLoaded) {
            return;
        }

        using Stream stream = await DataResolver.Shared.GetResource("__meta__");
        while (stream.Position < stream.Length) {
            ulong hash = stream.Read<ulong>();
            BlockResourceMetadataEntry entry = new() {
                BlockId = stream.Read<ulong>(),
                Offset = stream.Read<long>(),
                Size = stream.Read<int>()
            };

            Entries.Add(hash, entry);
        }

        _isLoaded = true;

        await Console.Out.WriteLineAsync(Entries.Count.ToString());
    }
}
