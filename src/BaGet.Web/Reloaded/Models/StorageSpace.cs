using System.IO;
using System.Reflection;

namespace BaGet.Web.Reloaded.Models;

/// <summary>
/// Contains information about server storage.
/// </summary>
/// <remarks>Included in this project as this is strictly UI only.</remarks>
public struct LocalStorageSpace
{
    private static readonly string _thisAssemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    public long BytesConsumed { get; set; }

    public long TotalBytes { get; set; }

    public string BytesConsumedGB => ToGigaBytesString(BytesConsumed);
    public string TotalBytesGB => ToGigaBytesString(TotalBytes);

    public LocalStorageSpace()
    {
        var drive = new DriveInfo(new DirectoryInfo(_thisAssemblyDirectory).Root.FullName);
        BytesConsumed = drive.TotalSize - drive.AvailableFreeSpace;
        TotalBytes = drive.TotalSize;
    }

    private static string ToGigaBytesString(long bytes) => (bytes / 1000000000.0f).ToString("#.00");
}
