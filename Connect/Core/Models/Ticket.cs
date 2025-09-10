using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using JetBrains.Annotations;

namespace SAMS.Connect.Core.Models;

public sealed partial class Ticket
    : ObservableValidator
{
#region Schema

    [ObservableProperty]
    private Guid _id = Guid.NewGuid();


    [ObservableProperty]
    private DateTime _createdAt = DateTime.UtcNow;


    [ObservableProperty]
    private DateTime _updatedAt = DateTime.UtcNow;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CompletedFieldsCount))]
    [Required(ErrorMessage = "This field is required and must be filled in.")]
    [MinLength(7, ErrorMessage = "Field must be at least 5 characters long.")]
    [MaxLength(100, ErrorMessage = "Field must be less than 100 characters long.")]
    private string _faultLocation = string.Empty;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CompletedFieldsCount))]
    [Required(ErrorMessage = "This field is required and must be filled in.")]
    [MinLength(5, ErrorMessage = "Field must be at least 5 characters long.")]
    [MaxLength(600, ErrorMessage = "Field must be less than 600 characters long.")]
    private string _faultDescription = string.Empty;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CompletedFieldsCount))]
    [Required(ErrorMessage = "You must select the relevant municipal department.")]
    private Department? _faultCategory;


    /* Attribution: Generic List (Collection)
     * - https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-8.0
     */
    [ObservableProperty]
    private List<string> _faultAttachments = [];

#endregion

#region Helper

    [UsedImplicitly]
    public int CompletedFieldsCount
    {
        get {
            var completed = 0;
            if (!string.IsNullOrWhiteSpace(FaultLocation)) completed++;
            if (!string.IsNullOrWhiteSpace(FaultDescription)) completed++;
            if (FaultCategory.HasValue) completed++;
            return completed;
        }
    }


    [UsedImplicitly]
    public void StartEntity() {
        CreatedAt = DateTime.UtcNow;
        TouchEntity();
    }


    [UsedImplicitly]
    public void TouchEntity() {
        UpdatedAt = DateTime.UtcNow;
    }


    public void ValidateAll() =>
        ValidateAllProperties();

#endregion
}
