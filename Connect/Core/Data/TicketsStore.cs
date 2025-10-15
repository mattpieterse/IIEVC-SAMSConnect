using CommunityToolkit.Mvvm.ComponentModel;
using SAMS.Connect.Core.Models;

namespace SAMS.Connect.Core.Data;

public sealed class TicketsStore
    : ObservableObject
{
#region Data

    private readonly List<Ticket> _store = [];
    public IEnumerable<Ticket> Store => _store;

#endregion

#region Crud

    public void Insert(Ticket instance) {
        instance.StartEntity();
        
        /* Attribution: Add Method (List)
         * - https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.add?view=net-8.0
         */
        _store.Add(instance);
        
        /* Attribution: Sort Method (List)
         * - https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.sort?view=net-8.0
         */
        _store.Sort((x, y) =>
            DateTime.Compare(x.CreatedAt, y.CreatedAt)
        );

        OnPropertyChanged(nameof(Store));
    }

#endregion
}
