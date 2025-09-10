using System.Collections.ObjectModel;
using SAMS.Connect.Core.Models;

namespace SAMS.Connect.MVVM.Models;

public sealed class TicketForm
{
#region Fields

    public Ticket Entity { get; } = new();
    public ObservableCollection<Attachment> Attachments { get; } = [];

#endregion
}
