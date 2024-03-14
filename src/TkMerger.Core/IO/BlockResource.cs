using CommunityToolkit.HighPerformance.Buffers;
using Standart.Hash.xxHash;
using System.Runtime.InteropServices;

namespace TkMerger.Core.IO;

public readonly ref struct BlockResource
{
    private readonly SpanOwner<byte> _owner;

    public readonly Span<byte> Data => _owner.Span;

    public BlockResource(ReadOnlySpan<char> resourceName)
    {
        ReadOnlySpan<byte> utf8 = MemoryMarshal.Cast<char, byte>(resourceName);
        ulong hash = xxHash64.ComputeHash(utf8, utf8.Length);
        BlockResourceMetadataEntry metadata = BlockResourceMetadata.Entries[hash];

        string blockFile = Path.Combine("Resources", metadata.BlockId.ToString());
        if (!File.Exists(blockFile)) {
            throw new FileNotFoundException($"""
                The block file '{metadata.BlockId}' could not be found.
                """);
        }

        _owner = SpanOwner<byte>.Allocate(metadata.Size);

        using FileStream fs = File.OpenRead(blockFile);
        fs.Seek(metadata.Offset, SeekOrigin.Begin);
        fs.Read(_owner.Span);
    }

    public readonly void Dispose()
    {
        _owner.Dispose();
    }
}
