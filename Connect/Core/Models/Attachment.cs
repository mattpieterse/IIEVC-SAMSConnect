using CommunityToolkit.Mvvm.ComponentModel;
using Path = System.IO.Path;

namespace SAMS.Connect.Core.Models;

public sealed partial class Attachment
    : ObservableValidator
{
#region Schema

    [ObservableProperty]
    private string _fileName = string.Empty;


    [ObservableProperty]
    private string _filePath = string.Empty;

#endregion

#region Serialization

    public Attachment(
        string localFilePath
    ) {
        if (string.IsNullOrEmpty(localFilePath))
            throw new ArgumentNullException(nameof(localFilePath));

        FilePath = localFilePath;
        FileName = Path.GetFileName(
            localFilePath
        );
    }

#endregion
}
