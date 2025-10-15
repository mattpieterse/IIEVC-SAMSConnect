namespace SAMS.Connect.Core.Models;

public static class LocalUpdateStoreExtensions
{
#region Extensions

    public static IEnumerable<LocalUpdate> FilterByDateRange(
        this IEnumerable<LocalUpdate> updates,
        DateTime? fromDate = null,
        DateTime? toDate = null
    ) {
        if (!fromDate.HasValue && !toDate.HasValue)
            return updates;

        return updates.Where(update =>
            (!fromDate.HasValue || update.CreatedAt.Date >= fromDate.Value.Date) &&
            (!toDate.HasValue || update.CreatedAt.Date <= toDate.Value.Date));
    }


    public static IEnumerable<LocalUpdate> FilterByDepartment(
        this IEnumerable<LocalUpdate> updates,
        Department? department
    ) {
        if (!department.HasValue)
            return updates;

        return updates.Where(update => update.UpdateCategory == department);
    }


    public static IEnumerable<LocalUpdate> FilterByType(
        this IEnumerable<LocalUpdate> updates,
        bool? isEvent
    ) {
        if (!isEvent.HasValue)
            return updates;

        return updates.Where(update => update.IsEvent == isEvent.Value);
    }


    public static IEnumerable<LocalUpdate> ApplyFilters(
        this IEnumerable<LocalUpdate> updates,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        Department? department = null,
        bool? isEvent = null
    ) {
        return updates
            .FilterByDateRange(fromDate, toDate)
            .FilterByDepartment(department)
            .FilterByType(isEvent);
    }

#endregion
}
