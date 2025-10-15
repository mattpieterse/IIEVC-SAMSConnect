using SAMS.Connect.Core.Models;

namespace SAMS.Connect.MVVM.Models;

public sealed class LocalUpdateWrapper
{
    private readonly bool _isRecommended;

    public LocalUpdateWrapper(LocalUpdate update, bool isRecommended) {
        Instance = update;
        _isRecommended = isRecommended;
    }

    public LocalUpdate Instance { get; }
}
