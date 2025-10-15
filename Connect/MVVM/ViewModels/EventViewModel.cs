using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JetBrains.Annotations;
using SAMS.Connect.Core.Data;
using SAMS.Connect.Core.Models;
using SAMS.Connect.Core.Services;
using SAMS.Connect.MVVM.Models;
using LocalUpdateFilter = SAMS.Connect.MVVM.Models.LocalUpdateFilter;

namespace SAMS.Connect.MVVM.ViewModels;

[UsedImplicitly]
public sealed partial class EventViewModel
    : ObservableObject
{
#region Lifecycle

    private readonly NavigationService _navigationService;
    private readonly UpdateStore _context;


    /// <summary>
    /// Standard class constructor.
    /// </summary>
    public EventViewModel(
        NavigationService navigationService,
        UpdateStore context
    ) {
        _context = context;
        _navigationService = navigationService;

        Filter.PropertyChanged += (_, e) => {
            ApplyFilters();
            if (e.PropertyName == nameof(LocalUpdateFilter.Department)) {
                ClearFilterCategoryCommand.NotifyCanExecuteChanged();
                if (Filter.Department is not null) {
                    _context.LogCategoryFilterUsage((Department) Filter.Department);
                }
            }
        };

        _context.Seed();
        ApplyFilters();
    }

#endregion

#region Variables

    private readonly LocalUpdateFilter _filter = new();
    public LocalUpdateFilter Filter => _filter;


    [ObservableProperty]
    private ObservableCollection<LocalUpdateWrapper> _collection = [];


    public static ObservableCollection<Department> DepartmentOptions
        => new(Enum.GetValues(typeof(Department)).Cast<Department>());

#endregion

#region Internals

    private void ApplyFilters() {
        var filteredCollection = _context.FetchAll()
            .ApplyFilters(
                _filter.DateStart,
                _filter.DateFinal,
                _filter.Department,
                _filter.IsEvent
            );

        Collection = new ObservableCollection<LocalUpdateWrapper>(
            filteredCollection.Select((instance) => new LocalUpdateWrapper(
                instance,
                _context.IsRecommended(instance)
            ))
        );
    }

#endregion

#region ICommands

    [RelayCommand]
    private void NavigateHome() => _navigationService.NavigateTo<HomeViewModel>();


    private bool CanClearFilterCategory() => Filter.Department != null;


    [RelayCommand(CanExecute = nameof(CanClearFilterCategory))]
    private void ClearFilterCategory() {
        Filter.Department = null;
    }

#endregion
}
