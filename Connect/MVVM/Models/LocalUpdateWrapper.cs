using SAMS.Connect.Core.Models;

namespace SAMS.Connect.MVVM.Models;

public sealed class LocalUpdateWrapper
{
    public LocalUpdateWrapper(LocalUpdate instance, bool isRecommended) {
        Instance = instance;
        IsRecommended = isRecommended;
    }

    public LocalUpdate Instance { get; }
    public bool IsRecommended { get; }
}
