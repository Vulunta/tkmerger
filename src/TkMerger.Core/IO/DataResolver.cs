namespace TkMerger.Core.IO;

public abstract class DataResolver
{
    private static DataResolver? _shared;
    public static DataResolver Shared {
        get => _shared ?? throw new InvalidOperationException("""
            DataResolver must be implemented by a frontend.
            """);
        set => _shared = value;
    }

    public abstract Task<Stream> GetResource(string resourceName);
    public abstract void WriteBytes(string file, Span<byte> data);
}
