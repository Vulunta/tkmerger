using Standart.Hash.xxHash;
using System.Buffers;
using System.Runtime.InteropServices;

namespace TkMerger.Core.IO;

public class BlockResource : IDisposable
{
    private readonly byte[] _buffer;
    private readonly int _size;

    private BlockResource(byte[] buffer, int size)
    {
        _buffer = buffer;
        _size = size;
    }

    public static async Task<BlockResource> Open(string resourceName)
    {
        BlockResourceMetadataEntry metadata = BlockResourceMetadata.Entries[GetKey(resourceName)];
        Stream stream = await DataResolver.Shared.GetResource(metadata.BlockId.ToString());

        byte[] buffer = ArrayPool<byte>.Shared.Rent(metadata.Size);
        stream.Seek(metadata.Offset, SeekOrigin.Begin);
        stream.Read(buffer.AsSpan()[..metadata.Size]);
        return new(buffer, metadata.Size);
    }

    public Span<byte> GetData()
    {
        return _buffer.AsSpan()[.._size];
    }

    private static ulong GetKey(ReadOnlySpan<char> resourceName)
    {
        ReadOnlySpan<byte> utf8 = MemoryMarshal.Cast<char, byte>(resourceName);
        return xxHash64.ComputeHash(utf8, utf8.Length);
    }

    public void Dispose()
    {
        ArrayPool<byte>.Shared.Return(_buffer);
        GC.SuppressFinalize(this);
    }
}
