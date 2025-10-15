using CommunityToolkit.Mvvm.ComponentModel;
using SAMS.Connect.Core.Models;

namespace SAMS.Connect.MVVM.Models;

public sealed partial class LocalUpdateFilter
    : ObservableObject
{
#region Variables

    [ObservableProperty]
    private DateTime? _dateStart;


    [ObservableProperty]
    private DateTime? _dateFinal;


    [ObservableProperty]
    private Department? _department;


    [ObservableProperty]
    private bool? _isEvent;

#endregion

#region Functions

    public void Clear() {
        DateStart = null;
        DateFinal = null;
        Department = null;
        IsEvent = null;
    }

#endregion
}
