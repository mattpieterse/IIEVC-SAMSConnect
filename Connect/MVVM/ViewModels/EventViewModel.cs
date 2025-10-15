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
#region Variables

    private readonly NavigationService _navigationService;
    private readonly UpdateStore _context;

    public EventViewModel(
        NavigationService navigationService,
        UpdateStore context
    ) {
        _context = context;
        _navigationService = navigationService;

        Filter.PropertyChanged += (_, _) => ApplyFilters();

        _context.Seed();
        ApplyFilters();
    }

    private readonly LocalUpdateFilter _filter = new();
    public LocalUpdateFilter Filter => _filter;


    [ObservableProperty]
    private ObservableCollection<LocalUpdateWrapper> _collection = [];

//    public DateOnly? FilterDateStart
//    {
//        get => _filter.DateStart.HasValue ? DateOnly.FromDateTime(_filter.DateStart.Value) : null;
//        set {
//            _filter.DateStart = value?.ToDateTime(TimeOnly.MinValue);
//            OnPropertyChanged(nameof(Collection));
//            ApplyFilters();
//        }
//    }
//
//    public DateOnly? FilterDateFinal
//    {
//        get => _filter.DateFinal.HasValue ? DateOnly.FromDateTime(_filter.DateFinal.Value) : null;
//        set {
//            _filter.DateFinal = value?.ToDateTime(TimeOnly.MaxValue);
//            OnPropertyChanged(nameof(Collection));
//            ApplyFilters();
//        }
//    }
//
//    public Department? SelectedDepartment
//    {
//        get => _filter.Department;
//        set {
//            if (_filter.Department == value) {
//                return;
//            }
//
//            _filter.Department = value;
//            if (value.HasValue) {
//                _context.LogCategoryFilterUsage(value.Value);
//            }
//
//            OnPropertyChanged();
//            ClearFilterCategoryCommand.NotifyCanExecuteChanged();
//            ApplyFilters();
//        }
//    }


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
