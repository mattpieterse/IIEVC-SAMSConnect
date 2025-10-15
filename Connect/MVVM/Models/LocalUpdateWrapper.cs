using SAMS.Connect.Core.Models;

namespace SAMS.Connect.MVVM.Models;

public sealed class LocalUpdateWrapper(
    LocalUpdate instance,
    bool isRecommended
)
{
    public LocalUpdate Instance { get; } = instance;
    public bool IsRecommended { get; } = isRecommended;
}
