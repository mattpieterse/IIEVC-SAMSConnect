using CommunityToolkit.Mvvm.ComponentModel;

namespace SAMS.Connect.Core.Models;

public sealed partial class LocalUpdate
    : ObservableObject
{
#region Schema

    [ObservableProperty]
    private Guid _id = Guid.NewGuid();


    [ObservableProperty]
    private DateTime _createdAt = DateTime.UtcNow;


    [ObservableProperty]
    private DateTime _updatedAt = DateTime.UtcNow;


    [ObservableProperty]
    private string _updateHeading = string.Empty;


    [ObservableProperty]
    private string _updateMessage = string.Empty;


    [ObservableProperty]
    private Department? _updateCategory;


    [ObservableProperty]
    private bool _isEvent;

#endregion

#region Constructors

    public LocalUpdate() { }


    public LocalUpdate(
        Guid id,
        DateTime createdAt,
        DateTime updatedAt,
        string updateHeading,
        string updateMessage,
        Department updateCategory,
        bool isEvent
    ) {
        _id = id;
        _createdAt = createdAt;
        _updatedAt = updatedAt;
        _updateHeading = updateHeading;
        _updateMessage = updateMessage;
        _updateCategory = updateCategory;
        _isEvent = isEvent;
    }

#endregion
}
