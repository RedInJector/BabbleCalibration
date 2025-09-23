using Godot;

namespace BabbleCalibration.Scripts.Backends;

public partial class OpenXRBackend : Node, IBackend
{
    public Node Self => this;
    public bool IsOverlay => false;
    public static IBackend Create()
    {
        throw new System.NotImplementedException();
    }
    public void Initialize()
    {
        throw new System.NotImplementedException();
    }
}